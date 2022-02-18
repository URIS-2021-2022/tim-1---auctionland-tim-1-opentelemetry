using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;

namespace ComplaintMicroservice.Profiles
{
    public class ComplaintConfirmationProfile : Profile
    {
        public ComplaintConfirmationProfile()
        {
            CreateMap<ComplaintConfirmation, ComplaintConfirmationDto>();
            CreateMap<Complaint, ComplaintConfirmation>();
            CreateMap<Complaint, ComplaintConfirmationDto>();
        }
    }
}
