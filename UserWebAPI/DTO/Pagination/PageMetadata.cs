namespace UserWebAPI.DTO.Pagination
{
    public class PageMetadata
    {
        public int Count { get; set; }
        public int Offset { get; set; }
        public long TotalElements { get; set; }

        public PageMetadata(Page page)
        {
            Count = page.Count;
            Offset = page.ElementsOffset;
            TotalElements = page.totalElements;
        }

    }

}
