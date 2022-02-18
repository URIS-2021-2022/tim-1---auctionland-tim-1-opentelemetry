using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;

namespace PublicBiddingMicroservice.Data
{
    public interface IStatusRepository
    {
        List<Status> GetStatuss();

        Status GetStatusById(Guid statusId);

        StatusConfirmation CreateStatus(Status status);

        void UpdateStatus(Status status);

        void DeleteStatus(Guid statusId);

        bool SaveChanges();
    }
}
