using AutoMapper;
using UserWebAPI.DTO;
using UserWebAPI.Models;

namespace UserWebAPI
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
