using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    public class StageCreationDto 
    {
        public Guid StageId { get; set; }

        public DateTime Date { get; set; }
    }
}
