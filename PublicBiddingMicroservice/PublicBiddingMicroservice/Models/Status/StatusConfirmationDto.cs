using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// DTO za status.
    /// </summary>
    public class StatusConfirmationDto
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
