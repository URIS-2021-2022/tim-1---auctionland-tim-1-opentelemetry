using PublicBiddingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Entities
{
    public class AuctionConfirmation
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
        [ForeignKey("Stage")]
        public Guid? StageId { get; set; }
        public Stage Stage { get; set; }

    }
}
