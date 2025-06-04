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

            CreateMap<UserRole, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));

            CreateMap<User, UserWithRolesDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles));
        }
    }
}
