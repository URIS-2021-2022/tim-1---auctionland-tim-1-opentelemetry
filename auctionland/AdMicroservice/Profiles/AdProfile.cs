using AdMicroservice.Entities;
using AdMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Profiles
{
    /// <summary>
    /// Kreirana klasa AdProfile
    /// </summary>
    public class AdProfile : Profile
    {
        /// <summary>
        /// Metoda koja vrši mapiranje 
        /// </summary>
        public AdProfile()
        {
            CreateMap<Ad, AdDto>();
            CreateMap<AdDto, Ad>();
            CreateMap<AdCreationDto, Ad>();
            CreateMap<AdUpdateDto, Ad>();
        }
    }
}
