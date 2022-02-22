using System;

namespace PublicBiddingMicroservice.Entities
{
    /// <summary>
    /// Predstavlja potvrdu kreiranja etape.
    /// </summary>
    public class StageConfirmation
    {
        /// <summary>
        /// Id etape
        /// </summary> 
        public Guid StageId { get; set; }

        /// <summary>
        /// Naziv etape
        /// </summary> 
        public DateTime Date { get; set; }
    }
}
