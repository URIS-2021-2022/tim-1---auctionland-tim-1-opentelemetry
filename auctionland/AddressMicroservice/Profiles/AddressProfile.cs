using AddressMicroservice.Etities;
using AddressMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Profiles
{
    public class AddressProfile : Profile 
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>()
                .ForMember(
                    dest => dest.Address,
                    opt => opt.MapFrom(src => src.Street + " " + src.Number))
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => src.CityName + ", " + src.ZipCode));
            CreateMap<Address, AddressCreationDto>();
            CreateMap<Address, AddressUpdateDto>();
        }
        
    }
}
