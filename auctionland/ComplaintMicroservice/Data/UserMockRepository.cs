using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ComplaintMicroservice.Entities;

namespace ComplaintMicroservice.Data
{
    public class UserMockRepository : IUserRepository
    {
        public static List<User> Users { get; set; } = new List<User>();
        private readonly static int iterations = 1000;

        public UserMockRepository()
        {
            FillData();
        }

        /// <summary>
        /// Upisuje testne podatke
        /// </summary>
        private static void FillData()
        {
            var user1 = HashPassword("testpassword");

            Users.AddRange(new List<User>
            {
                new User
                {
                    Id = Guid.Parse("CFD7FA84-8A27-4119-B6DB-5CFC1B0C94E1"),
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    UserName = "petar.petrovic",
                    Email = "petar.petrovic@testmail.com",
                    Password = user1.Item1,
                    Salt = user1.Item2
                }
            });
        }

        /// <summary>
        /// Vrši hešovanje korisničke lozinke
        /// </summary>
        /// <param name="password">Lozinka koja se hešuje</param>
        /// <returns>Generisan hash i salt</returns>
        private static Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];
            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
            var salt = Convert.ToBase64String(sBytes);

            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, iterations);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }

        /// <summary>
        /// Proverava da li postoji korisnik sa proslešenim korisničkim imenom i lozinkom
        /// </summary>
        /// <param name="username">Korisničko ime</param>
        /// <param name="password">Lozinka</param>
        /// <returns></returns>
        public bool UserWithCredentialsExists(string username, string password)
        {
            User user = Users.FirstOrDefault(u => u.UserName == username);

            if(user == null)
            {
                return false;
            }
            if(VerifyPassword(password, user.Password, user.Salt))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Proverava validnost prosleđene lozinke sa prosleđenim hešom
        /// </summary>
        /// <param name="password">Korisničko ime</param>
        /// <param name="savedHash">Sačuvani hash</param>
        /// <param name="savedSalt">Sačuvani salt</param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
            {
                return true;
            }
            return false;
        }
    }
}
