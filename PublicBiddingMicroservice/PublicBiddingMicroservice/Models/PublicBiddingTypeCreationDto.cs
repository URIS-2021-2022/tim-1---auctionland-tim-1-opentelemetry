using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    public class PublicBiddingTypeCreationDto
    {
        public Guid PublicBiddingTypeId { get; set; }

        public string PublicBiddingTypeName { get; set; }
    }
}
