using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class ParcelCreationDto
    {
        [Required(ErrorMessage = "Obavezno je uneti površinu parcele.")]
        public int SurfaceArea { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "Obavezno je uneti broj parcele.")]
        public string NumberOfParcel { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "Obavezno je uneti broj liste nepokretnosti.")]
        public string RealEstateListNumber { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrednost kulture.")]
        public string CultureRealStatus { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrednost klase.")]
        public string ClassRealStatus { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrstu obradivosti.")]
        public string WorkabilityActualStatus { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrednost statusa zaštićene zone.")]
        public string ProtectedZoneActualStatus { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti odgovarajući oblik svojine.")]
        public string FormOfOwnership { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću vrstu drenaže.")]
        public string DrainageActualCondition { get; set; }

        #region Municipality

        [Required(ErrorMessage = "Obavezno je uneti šifru opštine.")]
        public Guid MunicipalityID { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti naziv odgovarajuće opštine.")]
        public string NameOfTheMunicipality { get; set; }

        #endregion
    }
}
