using AutoMapper;
using ParcelMicroservice.Entities;
using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Profiles
{
    public class ParcelProfile : Profile
    {
        public ParcelProfile()
        {
            CreateMap<Parcel, ParcelDto>();//+
            CreateMap<ParcelCreationDto, Parcel>();//+
            CreateMap<Parcel, ParcelConfirmationDto>();
            CreateMap<ParcelUpdateDto, Parcel>();//+
            CreateMap<ParcelConfirmation, ParcelConfirmationDto>();//+
            CreateMap<Parcel, Parcel>();
            CreateMap<Parcel, ParcelConfirmation>();
        }
    }
}
