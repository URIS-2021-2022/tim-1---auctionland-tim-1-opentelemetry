﻿using PublicBiddingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Entities
{
    public class PublicBidding
    {
        /// <summary>
        /// Id javnog nadmetanja
        /// </summary> 
        public Guid PublicBiddingId { get; set; }

        /// <summary>
        /// Datum odrzavanja javnog nadmetanja
        /// </summary> 
        public DateTime Date { get; set; }

        /// <summary>
        /// Vreme početka javnog nadmetanja
        /// </summary> 
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Vreme kraja javnog nadmetanja
        /// </summary> 
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Početna cena po hektaru 
        /// </summary> 
        public double StartingPricePerHe { get; set; }

        /// <summary>
        /// Izuzeto  
        /// </summary> 
        public bool Exclude { get; set; }

        /// <summary>
        /// Izlicitirana Cena   
        /// </summary> 
        public double AuctionedPrice { get; set; }

        /// <summary>
        /// Period zakupa    
        /// </summary> 
        public int LeasePeriod { get; set; }

        /// <summary>
        /// Broj učesnika  
        /// </summary> 
        public int NumberOfParticipants { get; set; }

        /// <summary>
        /// Visina dopune depozita 
        /// </summary> 
        public double DepositReplenishment { get; set; }


        /// <summary>
        /// Krug javnog nadmetanja
        /// </summary> 
        public int Circle { get; set; }

        /// <summary>
        /// Status javnog nadmetanja
        /// </summary> 

        [ForeignKey("Status")]
        public Guid? StatusId { get; set; }
        public Status Status { get; set; }

        /// <summary>
        /// Etapa javnog nadmetanja
        /// </summary> 
        [ForeignKey("Stage")]
        public Guid? StageId { get; set; }
        public Stage Stage { get; set; }

        /// <summary>
        /// Tip javnog nadmetanja
        /// </summary> 
        [ForeignKey("PublicBiddingType")]
        public Guid? PublicBiddingTypeId { get; set; }
        public Stage PublicBiddingType { get; set; }

        /// <summary>
        /// Adresa javnog nadmetanja
        /// </summary> 
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        /// <summary>
        /// Parcela javnog nadmetanja
        /// </summary> 
        [ForeignKey("Parcel")]
        public Guid ParcelId { get; set; }

        [NotMapped]
        public AddressDto Address { get; set; }
       
        [NotMapped]
        public ParcelDto Parcel { get; set; }
    }
}
