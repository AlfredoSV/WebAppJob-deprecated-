namespace WebAppJob.Models
{
    public class DtoPaginationViewModel<T> where T : class
    {
        public PaginationViewModel PaginationViewModel { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
