namespace WebAppJob.Models
{
    public class DtoPaginationViewModel<T> where T : class
    {
        public PaginationViewModel PaginationViewModel { get; set; }

        public List<T> Data { get; set; }
    }
}
