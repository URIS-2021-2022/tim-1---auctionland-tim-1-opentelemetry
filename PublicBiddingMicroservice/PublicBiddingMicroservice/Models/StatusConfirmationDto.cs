using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    public class StatusConfirmationDto
    {
        public Guid StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
