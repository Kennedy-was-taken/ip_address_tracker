
namespace ip_address_tracker.Model
{
    public class ServiceModel <T>
    {
        public T? Data { get; set; }

        public bool isSuccess { get; set; }

        public string message { get; set; } = string.Empty;

        public int statusCode { get; set; }
    }
}
