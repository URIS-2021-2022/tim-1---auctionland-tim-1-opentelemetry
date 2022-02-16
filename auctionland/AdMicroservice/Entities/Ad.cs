using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    /// <summary>
    /// Kreirana klasa oglas
    /// </summary>
    public class Ad
    {
        /// <summary>
        /// ID oglasa
        /// </summary>
        public Guid AdID { get; set; }

        /// <summary>
        /// Naziv oglasa
        /// </summary>
        public string AdName { get; set; }

        #region officialList 

        /// <summary>
        /// ID službenog lista 
        /// </summary>
        public Guid OfficialListID { get; set; }

        /// <summary>
        /// Naziv opštine
        /// </summary>
        public string MunicipalityName { get; set; }

        /// <summary>
        /// Broj službenog lista 
        /// </summary>
        public string OfficialListNumber { get; set; }

        /// <summary>
        /// Datum izdavanja službenog lista 
        /// </summary>
        public DateTime DateOfIssue { get; set; }

        #endregion
    }
}
