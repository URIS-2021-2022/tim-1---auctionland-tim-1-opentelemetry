﻿using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Entities
{
    public class AuthorizedPerson
    {
        /// <summary>
        /// ID ovlascenog lica
        /// </summary>
        [Key]
        public Guid AuthorizedPersonID { get; set; }

        /// <summary>
        /// Ime ovlascenog lica
        /// </summary>
        //[Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime ovlascenog lica
        /// </summary>
        //[Required]
        public string LastName { get; set; }

        /// <summary>
        /// Broj licnog dokumenta (jmbg ili broj pasosa)
        /// </summary>
        //[Required]
        public string NumberPersonalDocument { get; set; }

        /// <summary>
        /// Broj table
        /// </summary>
        public string BoardTable { get; set; }

        /// <summary>
        /// Adresa ovlascenog lica
        /// </summary>
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        /// <summary>
        /// Javno nadmetanje ovlascenog lica
        /// </summary>
        [ForeignKey("PublicBidding")]
        public Guid PublicBiddingID { get; set; }

        [NotMapped]
        public PublicBiddingDto PublicBidding { get; set; }

        [NotMapped]
        public AddressDto Address { get; set; }
    }
}
