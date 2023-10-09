using UserWebAPI.DTO;
using UserWebAPI.DTO.Pagination;
using UserWebAPI.QueryExtensions.Filters;
using UserWebAPI.QueryExtensions.Params;
using UserWebAPI.QueryExtensions.Sorts;

namespace UserWebAPI.QueryExtensions.Responses
{
    public class UserQueryResponse
    {
        public PageMetadata PageMetadata { get; set; }
        public UserFilter? Filter { get; set; }
        public UserAttributeSorting? SortBy { get; set; }

        public ICollection<UserDto> Collection { get; set; }

        public UserQueryResponse(UserQueryParams queryParams, ICollection<UserDto> collection)
        {
            Collection = collection;
            Filter = queryParams.Filter;
            SortBy = queryParams.SortBy;

            PageMetadata = new PageMetadata(queryParams.Page);
        }

    }
}
