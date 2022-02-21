using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// DTO za status.
    /// </summary>
    public class StatusDto
    {
        public Guid StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
