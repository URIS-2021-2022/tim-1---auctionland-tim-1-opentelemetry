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

        public PublicBiddingType GetPublicBiddingTypeById(Guid stageId)
        {
            return context.PublicBiddingType.FirstOrDefault (e => e.PublicBiddingTypeId == stageId);
        }

        public PublicBiddingTypeConfirmation CreatePublicBiddingType(PublicBiddingType stage)
        {
            var createdEntity = context.Add(stage);
            return mapper.Map<PublicBiddingTypeConfirmation>(createdEntity.Entity);
        }                
        
        public void UpdatePublicBiddingType(PublicBiddingType stage)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeletePublicBiddingType(Guid stageId)
        {
            context.PublicBiddingType.Remove(GetPublicBiddingTypeById(stageId));
        }
    }
}
