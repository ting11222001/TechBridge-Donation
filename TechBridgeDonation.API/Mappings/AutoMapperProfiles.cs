using AutoMapper;
using TechBridgeDonation.API.Models.Domain;
using TechBridgeDonation.API.Models.DTO;

namespace TechBridgeDonation.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Organisation, OrganisationDto>().ReverseMap();
            CreateMap<AddOrganisationRequestDto, Organisation>().ReverseMap();
            CreateMap<UpdateOrganisationRequestDto, Organisation>().ReverseMap();
        }
    }
}
