using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Etities
{
    /// <summary>
    /// Predstavlja potvrdu adrese.
    /// </summary>
    public class AddressConfirmation
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

        /// <summary>
        /// Naziv grada.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Naziv države.
        /// </summary>
        public string CountryName { get; set; }

    }
}
