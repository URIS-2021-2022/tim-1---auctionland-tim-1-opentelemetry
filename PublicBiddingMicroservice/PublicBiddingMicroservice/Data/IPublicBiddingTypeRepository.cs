using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;

namespace PublicBiddingMicroservice.Data
{
    public interface IPublicBiddingTypeRepository
    {
        List<PublicBiddingType> GetPublicBiddingTypes();

        PublicBiddingType GetPublicBiddingTypeById(Guid publicBiddingTypeId);

        PublicBiddingTypeConfirmation CreatePublicBiddingType(PublicBiddingType publicBiddingType);

        void UpdatePublicBiddingType(PublicBiddingType publicBiddingType);

        void DeletePublicBiddingType(Guid publicBiddingTypeId);

        bool SaveChanges();
    }
}
