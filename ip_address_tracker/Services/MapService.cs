
using ip_address_tracker.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace ip_address_tracker.Services
{
    public class MapService 
    {

        private readonly string api_key = "apiKey=at_vaFBN1fKp81LTOLx8xs2Ix5V0OOJ1";
        private readonly string bare_url = "https://geo.ipify.org/api/v2/country,city?";

        public async Task<string> GetPublicIP()
        {
            string? publicIP = string.Empty;

            using (HttpClient client = new())
            {

                HttpResponseMessage response = await client.GetAsync("https://api.ipify.org?format=json");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    publicIP = (string) JObject.Parse(content)["ip"];

                }
            }

            return publicIP;
        }

        public async Task<ServiceModel<ContainerModel>> GetLocationIP(string ipAddress)
        {

            ServiceModel<ContainerModel>? serviceResponse = new();

            using (HttpClient client = new())
            {

                string url = bare_url + api_key + $"&ipAddress={ipAddress}";

                HttpResponseMessage response = await client.GetAsync(url);

                HttpStatusCode statusCode = response.StatusCode;
                int statusCodeNumber = (int)statusCode;

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    ContainerModel? dataContent = JsonConvert.DeserializeObject<ContainerModel>(content);

                    if (dataContent is not null)
                    {
                        serviceResponse.Data = dataContent;
                        serviceResponse.isSuccess = true;
                        serviceResponse.statusCode = statusCodeNumber;
                        serviceResponse.message = $"Success";
                    }

                }

                else if (statusCodeNumber.Equals(400))
                {
                    serviceResponse.Data = null;
                    serviceResponse.isSuccess = false;
                    serviceResponse.statusCode = statusCodeNumber;
                    serviceResponse.message = $"Could not find IP Address";
                }

                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.isSuccess = false;
                    serviceResponse.statusCode = statusCodeNumber;
                    serviceResponse.message = $"Ensure the right IP Address is entered";
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceModel<ContainerModel>> GetLocationDomain(string domain)
        {
            ServiceModel<ContainerModel>? serviceResponse = new();

            using (HttpClient client = new())
            {

                string url = bare_url + api_key + $"&domain={domain}";

                HttpResponseMessage response = await client.GetAsync(url);

                HttpStatusCode statusCode = response.StatusCode;
                int statusCodeNumber = (int)statusCode;

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    ContainerModel? dataContent = JsonConvert.DeserializeObject<ContainerModel>(content);

                    if (dataContent is not null)
                    {
                        serviceResponse.Data = dataContent;
                        serviceResponse.isSuccess = true;
                        serviceResponse.statusCode = statusCodeNumber;
                        serviceResponse.message = $"Success";
                    }

                }

                else if (statusCodeNumber.Equals(400))
                {
                    serviceResponse.Data = null;
                    serviceResponse.isSuccess = false;
                    serviceResponse.statusCode = statusCodeNumber;
                    serviceResponse.message = $"Could not find Domain name";
                }

                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.isSuccess = false;
                    serviceResponse.statusCode = statusCodeNumber;
                    serviceResponse.message = $"Ensure the right Domain name is entered";
                }
            }

            return serviceResponse;
        }

    }
}
