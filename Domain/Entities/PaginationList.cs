namespace Domain.Entities
{
    public class PaginationList<T>
    {
        public T Data {get; set;}

        public int Count {get; set;}
    }
}