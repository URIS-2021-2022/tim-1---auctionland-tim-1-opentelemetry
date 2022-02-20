using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public interface IPartOfParcelRepository
    {
        public List<PartOfParcelModel> GetPartOfParcels();
        public PartOfParcelModel GetPartOfParcelById(Guid ParcelID, Guid PartOfParcelID);
        public PartOfParcelConfirmation CreatePartOfParcel(PartOfParcelModel partOfParcel);
        public PartOfParcelConfirmation UpdatePartOfParcel(PartOfParcelModel partOfParcel);
        public void DeletePartOfParcel(Guid ParcelID, Guid PartOfParcelID);
    }
}
