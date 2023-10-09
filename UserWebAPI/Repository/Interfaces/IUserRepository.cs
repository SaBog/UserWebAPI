using UserWebAPI.Models;
using UserWebAPI.QueryExtensions.Params;

namespace UserWebAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAll(UserQueryParams queryParams);

        User GetById(int id);
        User? ContainsEmail(string email);

        void Create(User obj);
        void Update(User obj);
        void Delete(int id);
        void Save();
    }
}