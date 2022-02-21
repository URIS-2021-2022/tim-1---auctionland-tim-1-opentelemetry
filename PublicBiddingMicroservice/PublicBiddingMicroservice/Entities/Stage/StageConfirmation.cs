using System;

namespace PublicBiddingMicroservice.Entities
{
    /// <summary>
    /// Predstavlja potvrdu kreiranja etape.
    /// </summary>
    public class StageConfirmation
    {
        public Guid StageId { get; set; }

        public DateTime Date { get; set; }
    }
}
