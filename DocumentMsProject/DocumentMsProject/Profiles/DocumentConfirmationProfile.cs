using AutoMapper;
using DocumentMsProject.Entities;
using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Profiles
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
