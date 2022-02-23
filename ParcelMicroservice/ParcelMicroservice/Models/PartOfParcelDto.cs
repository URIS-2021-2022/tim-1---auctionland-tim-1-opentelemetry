using ParcelMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class PartOfParcelDto
    {
        /// <summary>
        /// Povrsina dela parcele
        /// </summary>
        public int SurfaceAreaPOP { get; set; }

        #region Land Class
        /// <summary>
        /// ID klase zemljista 
        /// </summary>
        public Guid ClassID { get; set; }

        /// <summary>
        /// Klasa zemljista 
        /// </summary>
        public string ClassLandLabel { get; set; }

        #endregion
    }
}
