using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Etities
{
    public class AddressConfirmation
    {
        //id adrese
        public Guid AddressID { get; set; }

        //Naziv ulice
        public string Street { get; set; }

        //Broj kuce/stana
        public int Number { get; set; }

        //Postanski broj
        public string ZipCode { get; set; }

        //grad
        public string CityName { get; set; }

        //drzava
        public string CountryName { get; set; }

    }
}
