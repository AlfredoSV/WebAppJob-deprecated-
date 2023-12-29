
namespace Domain
{
    public class DtoResponse<T>
    {
        private T _data;
        public T Data
        {
            set
            {

                if (value == null)
                    throw new NullReferenceException("value");
                _data = value;
            }

            get { return _data; }
        }
        public int Count { get; set; }
        public bool HasData => _data != null;
        public StatusRequest StatusRequestOp { get; set; }

        public static DtoResponse<T> Create(T data)
        {
            return new DtoResponse<T>() { _data = data };
        }

        public static DtoResponse<T> Create(T data,int count)
        {
            return new DtoResponse<T>() { _data = data, Count =  count};
        }

    }

    public class DtoResponse
    {
        public StatusRequest StatusRequestOp { get; set; }

        public static DtoResponse Create(StatusRequest status)
        {
            return new DtoResponse() { StatusRequestOp = status };
        }
    }

    public enum StatusRequest
    {
        Ok = 1,
        OperationNotPerformed = -1,
        Error = 0,
        DataNotFound = -2,
    }
}
