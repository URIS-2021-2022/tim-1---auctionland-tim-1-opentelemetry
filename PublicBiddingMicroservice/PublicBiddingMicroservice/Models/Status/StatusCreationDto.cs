using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje statusa.
    /// </summary>
    public class StatusCreationDto
    {
        public Guid StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
