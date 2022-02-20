using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class ParcelDto
    {
        public int SurfaceArea { get; set; }
        public string NumberOfParcel { get; set; }
        public string RealEstateListNumber { get; set; }
        public string CultureRealStatus { get; set; }
        public string ClassRealStatus { get; set; }
        public string WorkabilityActualStatus { get; set; }
        public string ProtectedZoneActualStatus { get; set; }
        public string FormOfOwnership { get; set; }
        public string DrainageActualCondition { get; set; }

        #region Municipality
        public Guid MunicipalityID { get; set; }
        public string NameOfTheMunicipality { get; set; }

        #endregion
    }
}
