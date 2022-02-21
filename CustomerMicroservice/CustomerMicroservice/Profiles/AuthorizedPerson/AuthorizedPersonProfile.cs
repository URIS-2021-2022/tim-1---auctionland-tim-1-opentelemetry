using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.AuthorizedPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class AuthorizedPersonProfile : Profile
    {
        public AuthorizedPersonProfile()
        {
            CreateMap<AuthorizedPerson, AuthorizedPersonDto>();
            CreateMap<AuthorizedPersonCreationDto, AuthorizedPerson>();
            CreateMap<AuthorizedPersonUpdateDto, AuthorizedPerson>();
            CreateMap<AuthorizedPerson, AuthorizedPerson>();
        }
    }
}
