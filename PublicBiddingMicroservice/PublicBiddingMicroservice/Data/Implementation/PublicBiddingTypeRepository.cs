using AutoMapper;
using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicBiddingMicroservice.Data
{
    public class PublicBiddingTypeRepository : IPublicBiddingTypeRepository
    {
        private readonly PublicBiddingContext context;
        private readonly IMapper mapper;

        public PublicBiddingTypeRepository(PublicBiddingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<PublicBiddingType> GetPublicBiddingTypes()
        {
            return context.PublicBiddingType.ToList();
        }

        public PublicBiddingType GetPublicBiddingTypeById(Guid publicBiddingTypeId)
        {
            return context.PublicBiddingType.FirstOrDefault (e => e.PublicBiddingTypeId == publicBiddingTypeId);
        }

        public PublicBiddingTypeConfirmation CreatePublicBiddingType(PublicBiddingType publicBiddingType)
        {
            var createdEntity = context.Add(publicBiddingType);
            return mapper.Map<PublicBiddingTypeConfirmation>(createdEntity.Entity);
        }                
        
        public void UpdatePublicBiddingType(PublicBiddingType publicBiddingType)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeletePublicBiddingType(Guid publicBiddingTypeId)
        {
            context.PublicBiddingType.Remove(GetPublicBiddingTypeById(publicBiddingTypeId));
        }
    }
}
