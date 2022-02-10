using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Models
{
    public class AddressUpdateDto
    {
        //id adrese
        public Guid AddressID { get; set; }

        //Naziv ulice
        public string Street { get; set; }

        //Broj kuce/stana
        public int Number { get; set; }

        //Postanski broj
        [Required(ErrorMessage = "Obavezno je uneti postanski broj.")]
        public string ZipCode { get; set; }

        #region City
        // id grada
        public Guid CityID { get; set; }

        //naziv grada 
        public string CityName { get; set; }

        #endregion

        #region Country
        //id drzave 
        public Guid CountryID { get; set; }

        //naziv drzave
        public string CountryName { get; set; }
        #endregion
    }
}
