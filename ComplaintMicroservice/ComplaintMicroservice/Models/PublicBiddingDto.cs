using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    public class PublicBiddingDto
    {
        public Guid PublicBiddingId { get; set; }

        public DateTime Date { get; set; }

        public int Circle { get; set; }
    }
}
