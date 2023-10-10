using AutoMapper;
using ManagingListUsers.Dto;
using ManagingListUsers.Models;

namespace ManagingListUsers.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<UserDto, User>();
            CreateMap<RoleDto, Role>();
        }
    }
}
