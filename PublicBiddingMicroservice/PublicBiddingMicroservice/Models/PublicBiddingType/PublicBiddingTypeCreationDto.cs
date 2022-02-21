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
        public Guid PublicBiddingTypeId { get; set; }

        public string PublicBiddingTypeName { get; set; }
    }
}
