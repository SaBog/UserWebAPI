using UserWebAPI.DTO;
using UserWebAPI.QueryExtensions.Params;
using UserWebAPI.QueryExtensions.Responses;

namespace UserWebAPI.Services.Interfaces
{
    public interface IRoleService
    {
        RoleQueryResponse GetAll(RoleQueryParams queryParams);

        RoleDto GetById(int id);

        void Create(RoleDto obj);
        void Update(RoleDto obj);
        void Delete(int id);
    }
}
