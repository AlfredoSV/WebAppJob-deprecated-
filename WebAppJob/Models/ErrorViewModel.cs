namespace WebAppJob.Models
{
    public class ErrorViewModel
    {
        public Guid RequestId { get; set; }

        public bool ShowRequestId => !(Guid.Empty == RequestId);
    }
}