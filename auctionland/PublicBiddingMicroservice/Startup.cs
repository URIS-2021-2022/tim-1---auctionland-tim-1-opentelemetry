using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using PublicBiddingMicroservice.Data;
using PublicBiddingMicroservice.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PublicBiddingMicroservice.Helpers;
using PublicBiddingMicroservice.Data.Impelmentation;
using Microsoft.EntityFrameworkCore;

namespace PublicBiddingMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //setup.ReturnHttpNotAcceptable = true -> Vraća status 406 (NotAcceptable) ukoliko klijent u Accept header-u zahteva traži neki format koji ne podržavamo (npr. application/xml)
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
                //Ovde sa setup.Filters mozemo dodati response tipove za sve kontrolere
                //setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));                
            }
            ).AddXmlDataContractSerializerFormatters() //Dodajemo podršku za XML tako da ukoliko klijent to traži u Accept header-u zahteva možemo da serializujemo payload u XML u odgovoru.
            .ConfigureApiBehaviorOptions(setupAction => //Deo koji se odnosi na podržavanje Problem Details for HTTP APIs
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    //Kreiramo problem details objekat
                    ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();

                    //Prosleđujemo trenutni kontekst i ModelState, ovo prevodi validacione greške iz ModelState-a u RFC format
                    ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                        context.HttpContext,
                        context.ModelState);

                    //Ubacujemo dodatne podatke
                    problemDetails.Detail = "Pogledajte polje errors za detalje.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    //po defaultu se sve vraća kao status 400 BadRequest, to je ok kada nisu u pitanju validacione greške,
                    //ako jesu hoćemo da koristimo status 422 UnprocessibleEntity
                    //tražimo info koji status kod da koristimo
                    var actionExecutiongContext = context as ActionExecutingContext;

                    //proveravamo da li postoji neka greška u ModelState-u, a takođe proveravamo da li su svi prosleđeni parametri dobro parsirani
                    //ako je sve ok parsirano ali postoje greške u validaciji hoćemo da vratimo status 422
                    if ((context.ModelState.ErrorCount > 0) &&
                        (actionExecutiongContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Type = "https://google.com"; //inače treba da stoji link ka stranici sa detaljima greške
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "Došlo je do greške prilikom validacije.";

                        //sve vraćamo kao UnprocessibleEntity objekat
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };

                    //ukoliko postoji nešto što nije moglo da se parsira hoćemo da vraćamo status 400 kao i do sada
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Došlo je do greške prilikom parsiranja poslatog sadržaja.";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Konfigurisanje Jwt autentifikacije za projekat
            //Registrujemo Jwt autentifikacionu shemu i definisemo sve potrebne Jwt opcije
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            /*
             * Izvor: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0#lifetime-and-registration-options
                - Transient objects are always different. The transient OperationId value is different in the IndexModel and in the middleware.
                - Scoped objects are the same for each request but different across each request.
                - Singleton objects are the same for every request.
               Full link: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0#overview-of-dependency-injection
             */
            //services.AddSingleton<IStageRepository, StageMockRepository>(); //Koristimo mock repozitorijum
            services.AddScoped<IStageRepository, StageRepository>(); //Koristimo konkretni repozitorijum
           // services.AddSingleton<IPublicBiddingRepository, PublicBiddingMockRepository>(); //Koristimo mock repozitorijum
            services.AddScoped<IPublicBiddingRepository, PublicBiddingRepository>(); //Koristimo konkretni repozitorijum
            //services.AddSingleton<IAuctionRepository, AuctionMockRepository>(); //Koristimo mock repozitorijum
            services.AddScoped<IAuctionRepository, AuctionRepository>(); //Koristimo konkretni repozitorijum
            services.AddSingleton<IUserAuthRepository, UserMockRepository>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("PublicBiddingOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Public Bidding API",
                        Version = "1",
                        //Često treba da dodamo neke dodatne informacije
                        Description = "Pomoću ovog API-ja može da se vrši kreiranje javnog nadmetanja kao i pregled i izmjena javnog nadmetanja",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Marko Marković",
                            Email = "marko@mail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        TermsOfService = new Uri("http://www.ftn.uns.ac.rs/publicBiddingTermsOfService")
                    });

                //Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
               // var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                //Pravimo putanju do XML fajla sa komentarima
               // var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
               // setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            //Dodajemo DbContext koji želimo da koristimo
            services.AddDbContext<PublicBiddingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PublicBiddingDB"))); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else //Ukoliko se nalazimo u Production modu postavljamo default poruku za greške koje nastaju na servisu
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Došlo je do neočekivane greške. Molimo pokušajte kasnije.");
                    });
                });
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                //Podesavamo endpoint gde Swagger UI moze da pronadje OpenAPI specifikaciju
                setupAction.SwaggerEndpoint("/swagger/PublicBiddingOpenApiSpecification/swagger.json", "Public Bidding API");
                setupAction.RoutePrefix = ""; //Dokumentacija ce sada biti dostupna na root-u (ne mora da se pise /swagger)
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
