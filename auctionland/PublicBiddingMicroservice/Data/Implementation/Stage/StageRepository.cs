using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicBiddingMicroservice.Data
{
    public class StageRepository : IStageRepository
    {
        private readonly PublicBiddingContext context;
        private readonly IMapper mapper;

        public StageRepository(PublicBiddingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Stage> GetStages()
        {
            return context.Stages.ToList();
        }

        public Stage GetStageById(Guid stageId)
        {
            return context.Stages.FirstOrDefault (e => e.StageId == stageId);
        }

        public StageConfirmation CreateStage(Stage stage)
        {
            var createdEntity = context.Add(stage);
            return mapper.Map<StageConfirmation>(createdEntity.Entity);
        }                
        
        public void UpdateStage(Stage stage)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteStage(Guid stageId)
        {
            context.Stages.Remove(GetStageById(stageId));
        }
    }
}
