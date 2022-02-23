using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Entities
{
    public class PartOfParcel
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
        /// ID klase zemljista 
        /// </summary>
        public Guid ClassID { get; set; }

        /// <summary>
        /// Klasa zemljista 
        /// </summary>
        public string ClassLandLabel { get; set; }

        #endregion

        /// <summary>
        /// Objekat parcele
        /// </summary>
        public Parcel Parcel { get; set; }
    }
}
