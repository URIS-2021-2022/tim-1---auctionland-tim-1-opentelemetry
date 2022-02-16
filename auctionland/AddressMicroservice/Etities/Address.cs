using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Etities
{
    /// <summary>
    /// Predstavlja adresu.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// ID adrese.
        /// </summary>
        public Guid AddressID { get; set; }

        /// <summary>
        /// Naziv ulice.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Broj kuće ili stana.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Poštanski broj.
        /// </summary>
        public string ZipCode { get; set; }

        #region City
        /// <summary>
        /// ID grada.
        /// </summary>
        public Guid CityID { get; set; }

        /// <summary>
        /// Naziv grada.
        /// </summary>        
        public string CityName { get; set; }

        #endregion

        #region Country
        /// <summary>
        /// ID države.
        /// </summary>
        public Guid CountryID { get; set; } 

        /// <summary>
        /// Naziv države.
        /// </summary>
        public string CountryName { get; set; }
        #endregion
    }
}
