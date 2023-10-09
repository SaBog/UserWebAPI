using UserWebAPI.Models;
using UserWebAPI.QueryExtensions.Params;

namespace UserWebAPI.Repository.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetAll(ICollection<int> ids);
        ICollection<Role> GetAll(RoleQueryParams queryParams);
        Role GetById(int id);

        void Create(Role obj);
        void Update(Role obj);
        void Delete(int id);
        void Save();
    }
}