using System.ComponentModel;

namespace UserWebAPI.DTO.Pagination
{
    public class Page
    {
        /// <summary>
        /// number of elements on page
        /// </summary>
        public int Count { get; set; } = 100;
        /// <summary>
        /// if query return 1000 item you can split it on 10 pages and write offset 
        /// (100, 200, 300, etc.)
        /// </summary>
        public int ElementsOffset { get; set; }

        public int totalElements;

        public static Page First => new(count: 100, offset: 0);

        public Page()
        {

        }

        public Page(int count, int offset)
        {
            Count = count;
            ElementsOffset = offset;
        }
    }

}
