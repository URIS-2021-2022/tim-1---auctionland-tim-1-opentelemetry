using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public interface IParcelRepository
    {
        public List<ParcelModel> GetParcels(string NumberOfParcel);
        public ParcelModel GetParcelById(Guid parcelID);
        public ParcelConfirmation CreateParcel(ParcelModel parcel);
        public ParcelConfirmation UpdateParcel(ParcelModel parcel);
        public void DeleteParcel(Guid parcelID);
    }
}
