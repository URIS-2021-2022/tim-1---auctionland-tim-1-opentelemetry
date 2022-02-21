﻿using AddressMicroservice.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AddressMicroservice.Data
{
    public class UserRepository : IUserRepository
    {
        public static List<User> users { get; set; } = new List<User>();
        private readonly static int iterations = 1000;

        public UserRepository()
        {
            FillData();
        }
        private void FillData()
        {
            var user1 = HashPassword("testpassword");

            users.AddRange(new List<User>
            {
                new User
                {
                    UserID = Guid.Parse("CFD7FA84-8A27-4119-B6DB-5CFC1B0C94E1"),
                    FirstName = "Milica",
                    LastName = "Djoric",
                    Username = "milica",
                    Email = "milica.djoric@testmail.com",
                    Password = user1.Item1,
                    Salt = user1.Item2
                }
            });
        }

        /// <summary>
        /// Proverava da li postoji korisnik sa prosleđenim kredencijalima
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserWithCredentialsExists(string username, string password)
        {
            //Ukoliko je username jedinstveno ovo je uredu
            User user = users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return false;
            }

            //Ako smo našli korisnika sa tim korisničkim imenom proveravamo lozinku
            if (VerifyPassword(password, user.Password, user.Salt))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Vrši hash-ovanje korisničke lozinke
        /// </summary>
        /// <param name="password">Korisnička lozinka</param>
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
        /// Proverava validnost prosleđene lozinke sa prosleđenim hash-om
        /// </summary>
        /// <param name="password">Korisnička lozinka</param>
        /// <param name="savedHash">Sačuvan hash</param>
        /// <param name="savedSalt">Sačuvan salt</param>
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