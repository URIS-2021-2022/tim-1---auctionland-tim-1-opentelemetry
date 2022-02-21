using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// DTO za tip javnog nadmetanja.
    /// </summary>
    public class PublicBiddingTypeDto
    {
        public Guid PublicBiddingTypeId { get; set; }

        public string PublicBiddingTypeName { get; set; }
    }
}
