using UserWebAPI.Controllers;
using UserWebAPI.DTO;
using UserWebAPI.QueryExtensions.Params;
using UserWebAPI.QueryExtensions.Responses;

namespace UserWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        UserQueryResponse GetAll(UserQueryParams queryParams);

        UserDto GetById(int id);

        void Create(UserDto obj);
        void Update(UserDto obj);
        void Delete(int id);
        string GetToken(string email);
    }
}
