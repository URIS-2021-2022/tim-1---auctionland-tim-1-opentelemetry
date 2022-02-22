using ParcelMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class PartOfParcelUpdateDto
    {
        public Guid ParcelID { get; set; }
        public Guid PartOfParcelID { get; set; }

        public int SurfaceAreaPOP { get; set; }

        #region Land Class

        [Required(ErrorMessage = "Obavezno je uneti šifru klase.")]
        public Guid ClassID { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću klasu zemljišta.")]
        public string ClassLandLabel { get; set; }

        #endregion

    }
}
