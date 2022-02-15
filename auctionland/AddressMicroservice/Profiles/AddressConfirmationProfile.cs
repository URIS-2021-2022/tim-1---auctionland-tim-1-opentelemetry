using AddressMicroservice.Etities;
using AddressMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Profiles
{
    public class AddressConfirmationProfile : Profile
    {
        public AddressConfirmationProfile()
        {
            CreateMap<AddressConfirmation, AddressConfirmationDto>();
            CreateMap<Address, AddressConfirmation>();
        }
    }
}
