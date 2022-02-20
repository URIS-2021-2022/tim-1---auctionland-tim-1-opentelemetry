using AutoMapper;
using ParcelMicroservice.Entities;
using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Profiles
{
    public class PartOfParcelProfile : Profile
    {
        public PartOfParcelProfile()
        {
            CreateMap<PartOfParcel, PartOfParcelDto>();
            CreateMap<PartOfParcelCreationDto, PartOfParcel>();
            CreateMap<PartOfParcelConfirmation, PartOfParcelConfirmationDto>();
            CreateMap<PartOfParcelUpdateDto, PartOfParcel>();
        }
    }
}
