﻿using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Entities
{
    public class CustomerConfirmation
    {
        public CustomerConfirmation() { }
        /// <summary>
        /// ID kupca
        /// </summary>
        [Key]
        public Guid CustomerID { get; set; }

        /// <summary>
        /// Da li je kupac fizicko lice, ako nije onda je pravno
        /// </summary>
        public bool IsPhysicalPerson { get; set; }

        /// <summary>
        /// Prioritet 
        /// </summary>
        //[Required]
        public string Priority { get; set; }

        /// <summary>
        /// Ostvarena povrsina
        /// </summary>
        //[Required]
        public string RealizedArea { get; set; }

        /// <summary>
        /// Da li kupac ima zabranu
        /// </summary>
        //[Required]
        public bool HasABan { get; set; }

        /// <summary>
        /// Datum pocetka zabrane
        /// </summary>
        public DateTime StartDateBan { get; set; }

        /// <summary>
        /// Duzina trajanja zabrane
        /// </summary>
        public int DurationBan { get; set; }

        /// <summary>
        /// Datum prestanka zabrane
        /// </summary>
        public DateTime EndDateBan { get; set; }

        /// <summary>
        /// Adresa kupca
        /// </summary>
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }


        /// <summary>
        /// Dokumenti kupca
        /// </summary>
        [ForeignKey("Document")]
        public Guid DocumentID { get; set; }

        /// <summary>
        /// Javno nadmetanje kupca
        /// </summary>
        [ForeignKey("PublicBidding")]
        public Guid PublicBiddingID { get; set; }

        [NotMapped]
        public PublicBiddingDto PublicBidding { get; set; }

        [NotMapped]
        public AddressDto Address { get; set; }

        [NotMapped]
        public DocumentDto Document { get; set; }

    }
}
