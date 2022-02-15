using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Profiles
{
    public class DocumentConfirmationProfile : Profile
    {
        public DocumentConfirmationProfile()
        {
            CreateMap<DocumentConfirmation, DocumentConfirmationDto>();
            CreateMap<Document, DocumentConfirmation>();
        }
    }
}
