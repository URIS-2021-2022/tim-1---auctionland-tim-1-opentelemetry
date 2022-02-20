using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class ParcelConfirmationDto
    {
        public Guid ParcelID { get; set; }
        public int SurfaceArea { get; set; }
        public string NumberOfParcel { get; set; }

        #region Municipality
        public string NameOfTheMunicipality { get; set; }

        #endregion

    }
}
