using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje etape.
    /// </summary>
    public class StageCreationDto 
    {
        public Guid StageId { get; set; }

        public DateTime Date { get; set; }
    }
}
