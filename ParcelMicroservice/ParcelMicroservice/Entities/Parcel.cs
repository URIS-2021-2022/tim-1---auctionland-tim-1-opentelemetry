using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Entities
{
    public class Parcel
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

        /// <summary>
        /// Lista nepokretnosti
        /// </summary>
        public string RealEstateListNumber { get; set; }

        /// <summary>
        /// Kultura realno stanje
        /// </summary>
        public string CultureRealStatus { get; set; }

        /// <summary>
        /// Klasa realno stanje 
        /// </summary>
        public string ClassRealStatus { get; set; }

        /// <summary>
        /// Obradivost stvarno stanje 
        /// </summary>
        public string WorkabilityActualStatus { get; set; }

        /// <summary>
        /// Zasticena zona stvarno stanje 
        /// </summary>
        public string ProtectedZoneActualStatus { get; set; }

        /// <summary>
        /// Oblik svojine
        /// </summary>
        public string FormOfOwnership { get; set; }

        /// <summary>
        /// Navodnjavanje stvarno stanje 
        /// </summary>
        public string DrainageActualCondition { get; set; }

        #region Municipality

        /// <summary>
        /// ID opstine
        /// </summary>
        public Guid MunicipalityID { get; set; }

        /// <summary>
        /// Naziv opstine
        /// </summary>
        public string NameOfTheMunicipality { get; set; }

        #endregion
    }
}
