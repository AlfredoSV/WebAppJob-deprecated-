namespace Domain.Entities
{
    public class CommonException : Exception
    {
        public override string Source { get; set; }
        public override string Message { get;  }

        public CommonException(string message, string source)
        {
            this.Message = message;
            this.Source = source;           
        }

    }
}
