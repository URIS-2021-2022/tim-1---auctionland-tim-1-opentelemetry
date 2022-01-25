using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UserMicroservice.Data;
using UserMicroservice.Helpers;
using UserMicroservice.Models;



namespace ExamRegistrationService.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;
        
        /*public AuthenticationHelper(IConfiguration configuration, IUserRepository userRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }*/

        /// <summary>
        /// Vrši autentifikaciju principala
        /// </summary>
        /// <param name="principal">Principal za autentifikaciju</param>
        /// <returns></returns>
        public bool AuthenticatePrincipal(Principal principal)
        {
            if (userRepository.UserWithCredentialsExists(principal.UserName, principal.Password))
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
