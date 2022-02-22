using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models
{
    /// <summary>
    /// DTO za javno nadmetanje 
    /// </summary>
    public class PublicBiddingDto
    {
        /// <summary>
        /// ID za javno nadmetanje
        /// </summary>
        public Guid PublicBiddingID { get; set; }
    }
}
