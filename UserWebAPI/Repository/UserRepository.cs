using Microsoft.EntityFrameworkCore;
using UserWebAPI.Data;
using UserWebAPI.Exceptions;
using UserWebAPI.Models;
using UserWebAPI.QueryExtensions.Filters;
using UserWebAPI.QueryExtensions.Params;
using UserWebAPI.QueryExtensions.Sorts;
using UserWebAPI.Repository.Interfaces;

namespace UserWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<User> GetAll(UserQueryParams queryParams)
        {
            IQueryable<User> query = _context.Users
                .Include(u => u.Roles) // included in filter method therefore queryParams should be normalized
                .AsQueryable();

            query = Filtration(query, queryParams.Filter);
            query = Sorting(query, queryParams.SortBy);

            queryParams.Page.totalElements = query.Count();

            return query
                .Skip(queryParams.Page.ElementsOffset)
                .Take(queryParams.Page.Count)
                .ToList();
        }

        private static IQueryable<User> Filtration(IQueryable<User> query, UserFilter filter)
        {
            if (filter.Id is not null)
            {
                query = query.Where(u => u.Id == filter.Id);
            }

            if (filter.Name is not null)
            {
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{filter.Name}%"));
            }

            if (filter.Email is not null)
            {
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email}%"));
            }

            if (filter.Age is not null)
            {
                query = query.Where(u => u.Age == filter.Age);
            }

            return query;
        }

        private static IQueryable<User> Sorting(IQueryable<User> query, UserAttributeSorting sorting)
        {
            if (sorting.Id != Direction.None)
            {
                query = sorting.Id == Direction.Asc ?
                    query.OrderBy(u => u.Id) :
                    query.OrderByDescending(u => u.Id);
            }

            if (sorting.Name != Direction.None)
            {
                query = sorting.Name == Direction.Asc ?
                    query.OrderBy(u => u.Name) :
                    query.OrderByDescending(u => u.Name);
            }

            if (sorting.Email != Direction.None)
            {
                query = sorting.Email == Direction.Asc ?
                    query.OrderBy(u => u.Email) :
                    query.OrderByDescending(u => u.Email);
            }

            if (sorting.Age != Direction.None)
            {
                query = sorting.Age == Direction.Asc ?
                    query.OrderBy(u => u.Age) :
                    query.OrderByDescending(u => u.Age);
            }

            return query;
        }

        public User GetById(int id)
        {
            var user = _context.Users
                .Include(u => u.Roles)
                .FirstOrDefault(x => x.Id == id);

            if (user is null)
            {
                throw new UserNotFoundException($"User with id = {id} not found");
            }

            return user;
        }

        public User? ContainsEmail(string email)
        {
            return _context.Users
                .Include(u => u.Roles.OrderBy(r => r.Id))
                .FirstOrDefault(u => u.Email == email);
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            _context.Remove(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
