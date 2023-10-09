using UserWebAPI.DTO.Pagination;
using UserWebAPI.Exceptions;
using UserWebAPI.QueryExtensions.Filters;
using UserWebAPI.QueryExtensions.Sorts;

namespace UserWebAPI.QueryExtensions.Params
{
    public class UserQueryParams
    {
        public Page? Page { get; set; }
        public UserFilter? Filter { get; set; }
        public UserAttributeSorting? SortBy { get; set; }

        public static UserQueryParams NoFilterNoSort => new()
        {
            Page = Page.First
        };

        internal UserQueryParams Fixed()
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
