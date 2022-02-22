using CustomerMicroservice.Data;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMicroservice
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
            services.AddDbContext<CustomerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CustomerDB")));

            services.AddControllers(setup =>
               setup.ReturnHttpNotAcceptable = true
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
                       problemDetails.Title = "Doslo je do greske prilikom validacije.";

                       //sve vraćamo kao UnprocessibleEntity objekat
                       return new UnprocessableEntityObjectResult(problemDetails)
                       {
                           ContentTypes = { "application/problem+json" }
                       };
                   }

                   //ukoliko postoji nešto što nije moglo da se parsira hoćemo da vraćamo status 400 kao i do sada
                   problemDetails.Status = StatusCodes.Status400BadRequest;
                   problemDetails.Title = "Doslo je do greske prilikom parsiranja poslatog sadrzaja.";
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

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPhysicalPersonRepository, PhysicalPersonRepository>();
            services.AddScoped<ILegallyPersonRepository, LegallyPersonRepository>();
            services.AddScoped<IAuthorizedPersonRepository, AuthorizedPersonRepository>();
            services.AddScoped<IAuthorizedPersonCustomerRepository, AuthorizedPersonCustomerRepository>();
            services.AddSingleton<IUserRepository, UserMockRepository>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            services.AddHttpContextAccessor();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("CustomerOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Customer API",
                        Version = "1",
                        Description = "Pomoću ovog API-ja može da se vrši dodavanje kupca, modifikacija ili brisanje kupaca.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Anđela Todorić",
                            Email = "andjelatodoric@gmail.com"
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
#pragma warning disable S1075
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
#pragma warning restore S1075
                        }
                    });

                //Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                //Pravimo putanju do XML fajla sa komentarima
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
                setupAction.IncludeXmlComments(xmlCommentsPath);

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Molim Vas unesite vaš token",
                    Name = "Autorizacija korisnika",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                    }
                });
            });
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                //Podesavamo endpoint gde Swagger UI moze da pronadje OpenAPI specifikaciju
                setupAction.SwaggerEndpoint("/swagger/CustomerOpenApiSpecification/swagger.json", "Customer API");
                setupAction.RoutePrefix = ""; //Dokumentacija ce sada biti dostupna na root-u (ne mora da se pise /swagger)
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
