using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje statusa.
    /// </summary>
    public class StatusCreationDto
    {
        /// <summary>
        /// Id statusa
        /// </summary>  
        public Guid StatusId { get; set; }

        /// <summary>
        /// Naziv statusa
        /// </summary>  
        public string StatusName { get; set; }
    }
}
