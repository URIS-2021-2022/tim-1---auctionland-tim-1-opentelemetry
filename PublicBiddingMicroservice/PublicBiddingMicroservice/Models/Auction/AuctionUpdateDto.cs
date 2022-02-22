using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// Model za azuriranje licitacije.
    /// </summary>
    public class AuctionUpdateDto
    {
        /// <summary>
        /// Id licitacije
        /// </summary>  
        public Guid AuctionId { get; set; }

        /// <summary>
        /// Id licitacije
        /// </summary> 
        public int Number { get; set; }

        /// <summary>
        /// Godina kada se licitacija izvodi
        /// </summary> 
        public int Year { get; set; }

        /// <summary>
        /// Datum licitacije
        /// </summary> 
        public DateTime Date { get; set; }

        /// <summary>
        /// Korak cene 
        /// </summary> 
        public double PriceIncrease { get; set; }

        /// <summary>
        /// Rok za dostavljanje prijava, datum i sat
        /// </summary> 
        public DateTime ApplicationDeadline { get; set; }

        /// <summary>
        /// Etapa licitacije
        /// </summary> 
        public Stage Stage { get; set; }

    }
}
