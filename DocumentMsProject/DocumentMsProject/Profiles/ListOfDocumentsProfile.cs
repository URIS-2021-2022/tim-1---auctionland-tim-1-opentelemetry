using AutoMapper;
using DocumentMsProject.Entities;
using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Profiles
{
    public class ListOfDocumentsProfile : Profile
    {
        public ListOfDocumentsProfile()
        {
            CreateMap<ListOfDocuments, ListOfDocumentsDto>();
            CreateMap<ListOfDocumentsCreationDto, ListOfDocuments>();
            CreateMap<ListOfDocumentsUpdateDto, ListOfDocuments>();
            CreateMap<ListOfDocuments, ListOfDocuments>();
        }
    }
}
