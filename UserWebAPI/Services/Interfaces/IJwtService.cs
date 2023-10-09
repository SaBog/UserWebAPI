using UserWebAPI.DTO;

namespace UserWebAPI.Services.Interfaces
{
    public interface IJwtService
    {
        string GetJwtToken(UserDto user);
    }
}