using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data
{
    /// <summary>
    /// Kreiran interfejs IUserRepository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Metoda koja proverava da li postoji korisnik sa prosleđenim kredencijalima
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool UserWithCredentialsExists(string username, string password);
    }
}
