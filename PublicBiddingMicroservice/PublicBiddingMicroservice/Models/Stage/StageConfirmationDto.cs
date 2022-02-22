using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// DTO za etapu.
    /// </summary>
    public class StageConfirmationDto 
    {
        /// <summary>
        /// Id etape
        /// </summary> 
        public Guid StageId { get; set; }

        /// <summary>
        /// Naziv etape
        /// </summary> 
        public DateTime Date { get; set; }
    }
}
