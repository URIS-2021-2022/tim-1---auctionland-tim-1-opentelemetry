using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Entities
{
    public class Stage
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
