using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// DTO za etapu.
    /// </summary>
    public class StageDto
    {
        public Guid StageId { get; set; }

        public DateTime Date { get; set; }

    }
}
