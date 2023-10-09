using System.ComponentModel.DataAnnotations.Schema;

namespace UserWebAPI.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }

        public ICollection<RoleDto> Roles { get; set; }
    }
}
