using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    /// <summary>
    /// DTO za kupca
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid CustomerID { get; set; }
    }
}
