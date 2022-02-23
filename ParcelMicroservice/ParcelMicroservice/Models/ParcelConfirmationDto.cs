using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class ParcelConfirmationDto
    {
        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelID { get; set; }

        /// <summary>
        /// Povrsina parcele
        /// </summary>
        public int SurfaceArea { get; set; }

        /// <summary>
        /// Broj parcele
        /// </summary>
        public string NumberOfParcel { get; set; }

        #region Municipality

        /// <summary>
        /// Naziv opstine
        /// </summary>
        public string NameOfTheMunicipality { get; set; }

        #endregion

    }
}
