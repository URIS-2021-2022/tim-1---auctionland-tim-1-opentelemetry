using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    /// <summary>
    /// Kreirana klasa AdConfirmation
    /// </summary>
    public class AdConfirmation
    {
        /// <summary>
        /// ID oglasa 
        /// </summary>
        public Guid AdID { get; set; }

        /// <summary>
        /// ID službenog lista 
        /// </summary>
        public Guid OfficialListID { get; set; }

        /// <summary>
        /// Datum izdavanja službenog lista 
        /// </summary>
        public DateTime DateOfIssue { get; set; }

    }
}
