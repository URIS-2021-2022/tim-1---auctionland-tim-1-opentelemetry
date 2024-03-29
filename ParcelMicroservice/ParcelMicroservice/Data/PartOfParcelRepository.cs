﻿using AutoMapper;
using ParcelMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public class PartOfParcelRepository : IPartOfParcelRepository
    {
        private readonly ParcelContext parcelContext;
        private readonly IMapper mapper;

        public PartOfParcelRepository(ParcelContext parcelContext, IMapper mapper)
        {
            this.parcelContext = parcelContext;
            this.mapper = mapper;
        }

        public PartOfParcelConfirmation CreatePartOfParcel(PartOfParcel partOfParcel)
        {
            var createdEntity = parcelContext.Add(partOfParcel);
            return mapper.Map<PartOfParcelConfirmation>(createdEntity.Entity);
        }

        public void DeletePartOfParcel(Guid PartOfParcelID)
        {
            var parcel = GetPartOfParcelById(PartOfParcelID);
            parcelContext.Remove(parcel);
        }

        public PartOfParcel GetPartOfParcelById(Guid PartOfParcelID)
        {
            return parcelContext.PartOfParcels.FirstOrDefault(e => e.PartOfParcelID == PartOfParcelID);
        }

        public List<PartOfParcel> GetPartOfParcels()
        {
            return parcelContext.PartOfParcels.ToList();
        }

        public bool SaveChanges()
        {
            return parcelContext.SaveChanges() > 0;
        }

        public void UpdatePartOfParcel(PartOfParcel partOfParcel)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
    }
}
