using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    /// <summary>
    /// Kreirana DTO klasa prilikom ažuriranja oglasa 
    /// </summary>
    public class AdUpdateDto
    {
        /// <summary>
        /// ID oglasa 
        /// </summary>
        public Guid AdID { get; set; }

        /// <summary>
        /// Naziv oglasa 
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv oglasa.")]
        public string AdName { get; set; }

        #region officialList 

        /// <summary>
        /// ID službenog lista 
        /// </summary>
        public Guid OfficialListID { get; set; }

        /// <summary>
        /// Naziv opštine 
        /// </summary>
        [MaxLength(15)]
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
