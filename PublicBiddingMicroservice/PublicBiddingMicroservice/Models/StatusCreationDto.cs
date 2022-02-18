using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    public class StatusCreationDto
    {
        public Guid StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
