using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// DTO za tip javnog nadmetanja.
    /// </summary>
    public class PublicBiddingTypeConfirmationDto
    {
        public Guid PublicBiddingTypeId { get; set; }

        public string PublicBiddingTypeName { get; set; }
    }
}
