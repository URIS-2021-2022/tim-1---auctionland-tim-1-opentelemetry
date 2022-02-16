using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;

namespace ComplaintMicroservice.Profiles
{
    public class ComplaintProfile : Profile
    {
        public ComplaintProfile()
        {
            CreateMap<Complaint, ComplaintDto>();
            CreateMap<ComplaintDto, Complaint>();
            CreateMap<ComplaintCreationDto, Complaint>();
            CreateMap<ComplaintUpdateDto, Complaint>();
        }
    }
}
