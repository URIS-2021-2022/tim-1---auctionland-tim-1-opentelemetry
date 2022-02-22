using AutoMapper;
using UserMicroservice.Models;

namespace UserMicroservice.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<UserCreationDto, DocumentDto>();

        }
    }
}
