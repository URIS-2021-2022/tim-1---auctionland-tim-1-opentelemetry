using ParcelMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class PartOfParcelCreationDto
    {
        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelID { get; set; }
        /// <summary>
        /// Povrsina dela parcele
        /// </summary>
        public int SurfaceAreaPOP { get; set; }

        #region Land Class

        /// <summary>
        /// ID klase zemljista 
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti šifru klase.")]
        public Guid ClassID { get; set; }

        /// <summary>
        /// Klasa zemljista 
        /// </summary>
        [MaxLength(15)]
        [Required(ErrorMessage = "Obavezno je uneti odgovarajuću klasu zemljišta.")]
        public string ClassLandLabel { get; set; }

        #endregion
    }
}
