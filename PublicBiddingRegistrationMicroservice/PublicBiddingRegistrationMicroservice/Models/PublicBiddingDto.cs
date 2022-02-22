using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    /// <summary>
    /// DTO za javno nadmetanje.
    /// </summary>
    public class PublicBiddingDto
    {
        public Guid PublicBiddingId { get; set; }
    }
}
