using AddressMicroservice.Etities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Models
{
    public class AddressDto
    {
        //Naziv ulice i broj 
        public string Address { get; set; }

        //grad i postanski broj
        public string City { get; set; }

        #region City
        // id grada
        public Guid CityID { get; set; }

        //naziv grada 
        //public string CityName { get; set; }

        #endregion

        #region Country
        //id drzave 
        public Guid CountryID { get; set; }

        //naziv drzave
        public string CountryName { get; set; }
        #endregion
    }
}
