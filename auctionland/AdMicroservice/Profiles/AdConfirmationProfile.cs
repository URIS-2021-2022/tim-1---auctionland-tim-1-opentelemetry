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
    /// Kreirana klasa AdConfirmationProfile 
    /// </summary>
    public class AdConfirmationProfile : Profile
    {
        /// <summary>
        /// Metoda koja vrši mapiranje 
        /// </summary>
        public AdConfirmationProfile()
        {
            CreateMap<AdConfirmation, AdConfirmationDto>();
        }
    }
}
