using PublicBiddingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    public class AddressDto
    {
        //Naziv ulice
        public string Street { get; set; }

        //Broj kuce/stana
        public int Number { get; set; }

        //Postanski broj
        public string ZipCode { get; set; }

    }
}
