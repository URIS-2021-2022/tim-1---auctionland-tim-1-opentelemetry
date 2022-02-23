using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class ParcelUpdateDto
    {
        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelID { get; set; }

        /// <summary>
        /// Povrsina parcele
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti površinu parcele.")]
        public int SurfaceArea { get; set; }

        /// <summary>
        /// Broj parcele
        /// </summary>
        [MaxLength(15)]
        [Required(ErrorMessage = "Obavezno je uneti broj parcele.")]
        public string NumberOfParcel { get; set; }

        /// <summary>
        /// Lista nepokretnosti
        /// </summary>
        [MaxLength(15)]
        [Required(ErrorMessage = "Obavezno je uneti broj liste nepokretnosti.")]
        public string RealEstateListNumber { get; set; }

        /// <summary>
        /// Kultura realno stanje
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrednost kulture.")]
        public string CultureRealStatus { get; set; }

        /// <summary>
        /// Klasa realno stanje 
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrednost klase.")]
        public string ClassRealStatus { get; set; }

        /// <summary>
        /// Obradivost stvarno stanje 
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrstu obradivosti.")]
        public string WorkabilityActualStatus { get; set; }

        /// <summary>
        /// Zasticena zona stvarno stanje 
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrednost statusa zaštićene zone.")]
        public string ProtectedZoneActualStatus { get; set; }

        /// <summary>
        /// Oblik svojine
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti odgovarajući oblik svojine.")]
        public string FormOfOwnership { get; set; }

        /// <summary>
        /// Navodnjavanje stvarno stanje 
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrstu drenaže.")]
        public string DrainageActualCondition { get; set; }

        #region Municipality

        /// <summary>
        /// ID opstine
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti šifru opštine.")]
        public Guid MunicipalityID { get; set; }

        /// <summary>
        /// Naziv opstine
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv odgovarajuće opštine.")]
        public string NameOfTheMunicipality { get; set; }

        #endregion
    }
}
