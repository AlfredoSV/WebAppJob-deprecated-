namespace WebAppJob.Models
{
    public class PaginationViewModel<T>
    {
        public IEnumerable<T> Data { get; set; }   
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
