using ParcelMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public interface IParcelRepository
    {
        public List<Parcel> GetParcels(string NumberOfParcel);
        public Parcel GetParcelById(Guid parcelID);
        public ParcelConfirmation CreateParcel(Parcel parcel);
        public ParcelConfirmation UpdateParcel(Parcel parcel);
        public void DeleteParcel(Guid parcelID);
    }
}
