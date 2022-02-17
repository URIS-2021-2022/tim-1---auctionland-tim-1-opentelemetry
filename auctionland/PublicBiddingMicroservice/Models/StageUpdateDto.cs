using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    public class StageUpdateDto 
    {
        public Guid StageId { get; set; }

        public DateTime Date { get; set; }
    }
}
