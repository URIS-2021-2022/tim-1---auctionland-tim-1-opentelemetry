using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// Model za azuriranje statusa.
    /// </summary>
    public class StatusUpdateDto
    {
        public Guid StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
