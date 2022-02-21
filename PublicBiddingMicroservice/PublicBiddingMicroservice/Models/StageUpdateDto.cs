using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// Model za azuriranje etape.
    /// </summary>
    public class StageUpdateDto 
    {
        public Guid StageId { get; set; }

        public DateTime Date { get; set; }
    }
}
