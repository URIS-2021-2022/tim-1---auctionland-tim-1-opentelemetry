using AddressMicroservice.Helpers;
using AddressMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication authenticationHelper;

        public AuthenticationController(IAuthentication authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// Sluzi za autentifikaciju korisnika
        /// </summary>
        /// <param name="principal">Model sa podacima na osnovu kojih se vrši autentifikacija</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate(Principal principal)
        {
            //Pokušaj autentifikacije
            if (authenticationHelper.AuthenticatePrincipal(principal))
            {
                var tokenString = authenticationHelper.GenerateJwt(principal);
                return Ok(new { token = tokenString });
            }

            //Ukoliko autentifikacija nije uspela vraća se status 401
            return Unauthorized();
        }
    }
}
