using System;

namespace PublicBiddingMicroservice.Entities
{

    public class StatusConfirmation
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
