using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicBiddingMicroservice.Data
{
    public class StageMockRepository : IStageRepository
    {
        public static List<Stage> Stages { get; set; } = new List<Stage>();

        public StageMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Stages.AddRange(new List<Stage>
            {
                new Stage
                {
                    StageId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Date = DateTime.Parse("2020-11-15T09:00:00")
                },
                new Stage
                {
                    StageId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Date = DateTime.Parse("2020-11-15T09:00:00")
                }
            });
        }

        public List<Stage> GetStages()
        {
            return (from e in Stages
                    select e).ToList();
        }

        public Stage GetStageById(Guid stageId)
        {
            return Stages.FirstOrDefault(e => e.StageId == stageId);
        }

        public StageConfirmation CreateStage(Stage stage)
        {
            stage.StageId = Guid.NewGuid();
            Stages.Add(stage);
            var exam = GetStageById(stage.StageId);

            return new StageConfirmation
            {
                StageId = exam.StageId,
                Date = exam.Date
            };
        }

        public void DeleteStage(Guid stageId)
        {
            Stages.Remove(Stages.FirstOrDefault(e => e.StageId == stageId));
        }

        public void UpdateStage(Stage stage)
        {
            var exam = GetStageById(stage.StageId);

            exam.StageId = stage.StageId;
            exam.Date = stage.Date;
        }

        //Koristi se za Update kako u kontroleru ne bi morali da menjamo logiku pri zameni repozitorijuma
        public bool SaveChanges()
        {            
            return true;
        }
    }
}
