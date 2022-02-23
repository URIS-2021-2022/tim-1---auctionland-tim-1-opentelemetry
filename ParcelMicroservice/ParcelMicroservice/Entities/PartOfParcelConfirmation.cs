using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Entities
{
    public class PartOfParcelConfirmation
    {
        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelID { get; set; }

        /// <summary>
        /// ID dela parcele
        /// </summary>
        public Guid PartOfParcelID { get; set; }

        /// <summary>
        /// Povrsina dela parcele
        /// </summary>
        public int SurfaceAreaPOP { get; set; }

        #region Land Class
        /// <summary>
        /// Klasa zemljista 
        /// </summary>
        public string ClassLandLabel { get; set; }

        #endregion
    }
}
