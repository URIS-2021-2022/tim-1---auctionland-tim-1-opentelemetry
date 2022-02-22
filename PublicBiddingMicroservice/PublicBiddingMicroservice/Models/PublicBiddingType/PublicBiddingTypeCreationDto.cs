using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// Model za krerianje tipa javnog nadmetanja.
    /// </summary>
    public class PublicBiddingTypeCreationDto
    {
        /// <summary>
        /// Id tipa javnog nadmetanja
        /// </summary> 
        public Guid PublicBiddingTypeId { get; set; }

        /// <summary>
        /// Naziv tipa javnog nadmetanja
        /// </summary> 
        public string PublicBiddingTypeName { get; set; }
    }
}
