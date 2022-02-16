using AdMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Helpers
{
    /// <summary>
    /// Kreiran interfejs IAuthenticationHelper
    /// </summary>
    public interface IAuthenticationHelper
    {
        /// <summary>
        /// Metoda za proveru principala korisnika 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        bool AuthenticatePrincipal(Principal principal);

        /// <summary>
        /// Metoda za generisanje tokena 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        string GenerateJwt(Principal principal);

    }
}
