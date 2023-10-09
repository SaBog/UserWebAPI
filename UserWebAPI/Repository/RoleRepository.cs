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
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Role> GetAll(ICollection<int> ids)
        {
            var roleIds = new HashSet<int>(ids);
            return _context.Roles
                .Where(r => roleIds.Contains(r.Id))
                .ToList();
        }

        public ICollection<Role> GetAll(RoleQueryParams queryParams)
        {
            IQueryable<Role> query = _context.Roles
                .AsQueryable();

            query = Filtration(query, queryParams.Filter);
            query = Sorting(query, queryParams.SortBy);

            queryParams.Page.totalElements = query.Count();

            return query
                .Skip(queryParams.Page.ElementsOffset)
                .Take(queryParams.Page.Count)
                .ToList();
        }

        private static IQueryable<Role> Filtration(IQueryable<Role> query, RoleFilter filter)
        {
            if (filter.Id is not null)
            {
                query = query.Where(r => r.Id == filter.Id);
            }

            if (filter.Name is not null)
            {
                query = query.Where(r => EF.Functions.Like(r.Name, $"%{filter.Name}%"));
            }

            return query;
        }

        private static IQueryable<Role> Sorting(IQueryable<Role> query, RoleAttributeSorting sorting)
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

            return query;
        }

        public Role GetById(int id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == id);

            if (role == null)
            {
                throw new RoleNotFoundException($"Role with id = {id} not found");
            }

            return role;
        }

        public void Create(Role role)
        {
            _context.Roles.Add(role);
        }

        public void Update(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var role = GetById(id);
            _context.Remove(role);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
