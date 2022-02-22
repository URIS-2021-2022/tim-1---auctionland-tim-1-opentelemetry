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
        /// <summary>
        /// Id statusa
        /// </summary>  
        public Guid StatusId { get; set; }

        /// <summary>
        /// Naziv statusa
        /// </summary>  
        public string StatusName { get; set; }
    }
}
