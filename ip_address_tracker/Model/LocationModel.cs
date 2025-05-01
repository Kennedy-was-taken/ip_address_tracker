
namespace ip_address_tracker.Model
{
    public class LocationModel
    {
        public string country { get; set; } = string.Empty;

        public string region { get; set; } = string.Empty;

        public string city { get; set; } = string.Empty;

        public double lat { get; set; } 

        public double lng { get; set; } 

        public string postalCode { get; set; } = string.Empty;

        public string timezone { get; set; } = string.Empty;

        public int geonameID { get; set; } 
    }
}
