using AdMicroservice.Data;
using AdMicroservice.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMicroservice.Helpers
{
    /// <summary>
    /// Kreirana klasa AuthenticationHelper
    /// </summary>
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Injektovanje zavisnosti preko konstruktora klase AuthenticationHelper
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userRepository"></param>
        public AuthenticationHelper(IConfiguration configuration, IUserRepository userRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Vrši autentifikaciju principala
        /// </summary>
        /// <param name="principal">Principal za autentifikaciju</param>
        /// <returns></returns>
        public bool AuthenticatePrincipal(Principal principal)
        {
            if (userRepository.UserWithCredentialsExists(principal.username, principal.password))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Generiše novi token za autentifikovanog principala
        /// </summary>
        /// <param name="principal">Autentifikovani principal</param>
        /// <returns></returns>
        public string GenerateJwt(Principal principal)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Issuer"],
                                             null,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
