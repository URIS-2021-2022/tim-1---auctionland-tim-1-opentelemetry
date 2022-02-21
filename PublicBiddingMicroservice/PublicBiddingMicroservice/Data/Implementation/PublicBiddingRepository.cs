using AutoMapper;
using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicBiddingMicroservice.Data
{
    public class PublicBiddingRepository : IPublicBiddingRepository
    {
        private readonly PublicBiddingContext context;
        private readonly IMapper mapper;

        public PublicBiddingRepository(PublicBiddingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<PublicBidding> GetPublicBiddings(int numberOfParticipants = 0)
        {
            return context.PublicBidding.Where(e => (numberOfParticipants == 0 || e.NumberOfParticipants == numberOfParticipants)).ToList();
        }

        public PublicBidding GetPublicBiddingById(Guid stageId)
        {
            return context.PublicBidding.FirstOrDefault(e => e.PublicBiddingId == stageId);
        }

        public PublicBiddingConfirmation CreatePublicBidding(PublicBidding stage)
        {
            var createdEntity = context.Add(stage);
            return mapper.Map<PublicBiddingConfirmation>(createdEntity.Entity);
        }

        public void UpdatePublicBidding(PublicBidding stage)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeletePublicBidding(Guid stageId)
        {
            context.PublicBidding.Remove(GetPublicBiddingById(stageId));
        }
    }
}
