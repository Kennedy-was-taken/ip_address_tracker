
using System.ComponentModel.DataAnnotations;

namespace ip_address_tracker.Model
{
    public class Container
    {
        public string ip { get; set; } = string.Empty;

        public LocationModel? location { get; set; }

        public List<string>? domains { get; set; }

        [Display(Name = "as")]
        public AsModel? As { get; set; }

        public string isp { get; set; } = string.Empty;
    }
}
