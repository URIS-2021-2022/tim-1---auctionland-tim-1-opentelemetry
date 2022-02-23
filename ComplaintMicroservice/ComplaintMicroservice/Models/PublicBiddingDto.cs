using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    public class PublicBiddingDto
    {
        /// <summary>
        /// Id javnog nadmetanja
        /// </summary>
        public Guid PublicBiddingId { get; set; }

        /// <summary>
        /// Datum održavanja javnog nadmetanja
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Krug javnog nadmetanja
        /// </summary>
        public int Circle { get; set; }
    }
}
