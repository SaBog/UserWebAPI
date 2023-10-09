using UserWebAPI.DTO.Pagination;
using UserWebAPI.Exceptions;
using UserWebAPI.QueryExtensions.Filters;
using UserWebAPI.QueryExtensions.Sorts;

namespace UserWebAPI.QueryExtensions.Params
{
    public class RoleQueryParams
    {
        public Page? Page { get; set; }
        public RoleFilter? Filter { get; set; }
        public RoleAttributeSorting? SortBy { get; set; }

        public static RoleQueryParams NoFilterNoSort => new()
        {
            Page = Page.First
        };

        internal RoleQueryParams Fixed()
        {
            Page ??= Page.First;
            Filter ??= new();
            SortBy ??= new();

            if (Page.Count <= 0 || Page.ElementsOffset < 0)
            {
                throw new QueryArgumentException("COUNT and OFFSET clause may not be negative");
            }

            return this;
        }
    }


}
