using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;

namespace PublicBiddingMicroservice.Data
{
    public interface IStageRepository
    {
        List<Stage> GetStages();

        Stage GetStageById(Guid stageId);

        StageConfirmation CreateStage(Stage stage);

        void UpdateStage(Stage stage);

        void DeleteStage(Guid stageId);

        bool SaveChanges();
    }
}
