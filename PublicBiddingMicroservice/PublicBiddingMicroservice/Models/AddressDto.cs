using PublicBiddingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    public class AddressDto
    {
        /// <summary>
        /// Naziv ulice i broj stana ili kuće.
        /// </summary>        
        public string Address { get; set; }

        /// <summary>
        /// Naziv grada i poštanski broj.
        /// </summary>
        public string City { get; set; }

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
