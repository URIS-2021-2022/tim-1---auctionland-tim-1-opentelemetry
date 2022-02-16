using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    interface IParcelaRepository
    {
        List<ParcelaModel> GetParcels(int parcelSurface = 0, List<DeoParceleModel> parcelParts = null);
        ParcelaModel GetParcelByID(Guid parcelID);
        ParcelaConfirmation CreateParcel(ParcelaModel parcelModel);
        ParcelaConfirmation UpdateParcel(ParcelaModel parcelModel);
        void DeleteParcel(Guid parcelID);

    }
}
