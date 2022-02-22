
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Entities
{
    public class PublicBiddingType
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
