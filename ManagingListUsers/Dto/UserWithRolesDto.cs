namespace ManagingListUsers.Dto
{
    public class UserWithRolesDto : UserDto
    {
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}
