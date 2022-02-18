using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    /// <summary>
    /// Kreirana klasa AdConfirmationDto, koja predstavlja potvrdu prilikom ažuriranja ili dodavanja novog oglasa
    /// </summary>
    public class AdConfirmationDto
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
