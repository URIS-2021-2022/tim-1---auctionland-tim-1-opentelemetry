using AutoMapper;
using ParcelMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public class ParcelRepository : IParcelRepository
    {
        private readonly ParcelContext parcelContext;
        private readonly IMapper mapper;

        public ParcelRepository(ParcelContext parcelContext, IMapper mapper)
        {
            this.parcelContext = parcelContext;
            this.mapper = mapper;
        }

        public ParcelConfirmation CreateParcel(Parcel parcel)
        {
            var createdEntity = parcelContext.Add(parcel);
            return mapper.Map<ParcelConfirmation>(createdEntity.Entity);
        }

        public void DeleteParcel(Guid parcelID)
        {
            var parcel = GetParcelById(parcelID);
            parcelContext.Remove(parcel);
        }

        public Parcel GetParcelById(Guid parcelID)
        {
            return parcelContext.Parcels.FirstOrDefault(e => e.ParcelID == parcelID);
        }

        public List<Parcel> GetParcels(string NumberOfParcel)
        {
            return parcelContext.Parcels.Where(e => string.IsNullOrEmpty(NumberOfParcel) || e.NumberOfParcel == NumberOfParcel).ToList();
        }

        public bool SaveChanges()
        {
            return parcelContext.SaveChanges() > 0;
        }

        public void UpdateParcel(Parcel parcel)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
    }
}
