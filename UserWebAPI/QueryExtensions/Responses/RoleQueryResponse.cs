using UserWebAPI.DTO;
using UserWebAPI.DTO.Pagination;
using UserWebAPI.QueryExtensions.Filters;
using UserWebAPI.QueryExtensions.Params;
using UserWebAPI.QueryExtensions.Sorts;

namespace UserWebAPI.QueryExtensions.Responses
{
    public class RoleQueryResponse
    {
        public PageMetadata PageMetadata { get; set; }
        public RoleFilter? Filter { get; set; }
        public RoleAttributeSorting? SortBy { get; set; }

        public ICollection<RoleDto> Collection { get; set; }

        public RoleQueryResponse(RoleQueryParams queryParams, ICollection<RoleDto> collection)
        {
            Collection = collection;
            Filter = queryParams.Filter;
            SortBy = queryParams.SortBy;

            PageMetadata = new PageMetadata(queryParams.Page);
        }

    }
}
