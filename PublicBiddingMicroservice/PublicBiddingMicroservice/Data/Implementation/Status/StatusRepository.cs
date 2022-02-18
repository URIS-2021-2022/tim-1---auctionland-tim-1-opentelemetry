using AutoMapper;
using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicBiddingMicroservice.Data
{
    public class StatusRepository : IStatusRepository
    {
        private readonly PublicBiddingContext context;
        private readonly IMapper mapper;

        public StatusRepository(PublicBiddingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Status> GetStatuss()
        {
            return context.Status.ToList();
        }

        public Status GetStatusById(Guid stageId)
        {
            return context.Status.FirstOrDefault (e => e.StatusId == stageId);
        }

        public StatusConfirmation CreateStatus(Status stage)
        {
            var createdEntity = context.Add(stage);
            return mapper.Map<StatusConfirmation>(createdEntity.Entity);
        }                
        
        public void UpdateStatus(Status stage)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteStatus(Guid stageId)
        {
            context.Status.Remove(GetStatusById(stageId));
        }
    }
}
