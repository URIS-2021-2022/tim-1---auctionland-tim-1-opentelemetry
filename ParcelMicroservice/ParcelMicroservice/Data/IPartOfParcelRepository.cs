using ParcelMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public interface IPartOfParcelRepository
    {
        public List<PartOfParcel> GetPartOfParcels();
        public PartOfParcel GetPartOfParcelById(Guid PartOfParcelID);
        public PartOfParcelConfirmation CreatePartOfParcel(PartOfParcel partOfParcel);
        public void UpdatePartOfParcel(PartOfParcel partOfParcel);
        public void DeletePartOfParcel(Guid PartOfParcelID);
        public bool SaveChanges();
    }
}
