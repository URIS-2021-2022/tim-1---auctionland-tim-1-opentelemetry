using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Profiles
{
    public class ListOfDocumentsProfile : Profile
    {
        public ListOfDocumentsProfile()
        {
            CreateMap<ListOfDocuments, ListOfDocumentsDto>();
            CreateMap<ListOfDocuments, ListOfDocumentsCreationDto>();
            CreateMap<ListOfDocuments, ListOfDocumentsUpdateDto>();
        }
    }
}
