using ip_address_tracker.Model;
using ip_address_tracker.Services;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;

namespace TestAPI.API
{
    [TestCaseOrderer(
    ordererTypeName: "TestAPI.PriorityOrderer",
    ordererAssemblyName: "TestAPI")]
    public class GetApi
    {
        MapService? mapService;

        private string ip_address = string.Empty;

        private string api_key = "apiKey=at_vaFBN1fKp81LTOLx8xs2Ix5V0OOJ1";
        private string bare_url = "https://geo.ipify.org/api/v2/country,city?";

        [Fact]
        [TestPriority(1)]
        public async Task TestTrue()
        {

            //var logger = LoggerFactory.Create();
            ServiceModel<ContainerModel> serviceResponds = new ServiceModel<ContainerModel>();

            using (HttpClient client = new())
            {

                string ip = "8.8.8.8";
                string url = bare_url + api_key + $"&ipAddress={ip}";

                HttpResponseMessage response = await client.GetAsync(url);
                HttpStatusCode statusCode = response.StatusCode;
                int statusCodeNumber = (int)statusCode;

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    ContainerModel? dataContent = JsonConvert.DeserializeObject<ContainerModel>(content);

                    if ( dataContent is not null && response.RequestMessage is not null) {

                        serviceResponds.Data = dataContent;
                        serviceResponds.isSuccess = true;
                        serviceResponds.statusCode = statusCodeNumber;
                        serviceResponds.message = "Success";

                    }
                }

                else if (response.StatusCode.Equals(400))
                {
                    serviceResponds.Data = null;
                    serviceResponds.isSuccess = false;
                    serviceResponds.statusCode = statusCodeNumber;
                    serviceResponds.message = $" : Could not find IP Address or Domain name";
                }

                else if (response.StatusCode.Equals(500))
                {
                    serviceResponds.Data = null;
                    serviceResponds.isSuccess = false;
                    serviceResponds.statusCode = statusCodeNumber;
                    serviceResponds.message = $" : Content Developer";
                }

            }

            if (serviceResponds.isSuccess)
            {
                Debug.WriteLine(serviceResponds.message);
                Assert.True(serviceResponds.isSuccess);
            }

            else
            {
                Debug.WriteLine(serviceResponds.message);
                Assert.Fail($"{serviceResponds.message}");
            }

        }

        [Fact]
        [TestPriority(2)]
        public async Task GetPublicIPTest()
        {
            mapService = new();

            string publicIP = await mapService.GetPublicIP();

            if (publicIP is not null || publicIP != "")
            {
                Console.WriteLine(publicIP);
                Assert.True(true);
            }

            else
            {
                Assert.Fail("public IP is null or empty");
            }

        }

        [Fact]
        [TestPriority(3)]
        public async Task GetLocationIPTest_True()
        {
            mapService = new();

            ServiceModel<ContainerModel>? serviceResponse = await mapService.GetLocationIP("8.8.8.8");

            if (serviceResponse.isSuccess)
            {
                
                Assert.True(serviceResponse.isSuccess);
            }

            else
            {
                Assert.Fail($"{serviceResponse.message}");
            }

        }

        [Fact]
        [TestPriority(4)]
        public async Task GetLocationIPTest_False()
        {
            mapService = new();

            ServiceModel<ContainerModel>? serviceResponse = await mapService.GetLocationIP("8.8");

            if (serviceResponse.isSuccess)
            {
                Assert.Fail($"{serviceResponse.message}");
                
            }

            else
            {
                Assert.False(serviceResponse.isSuccess);
            }

        }

        [Fact]
        [TestPriority(5)]
        public async Task GetLocationDomain_True()
        {
            mapService = new();

            ServiceModel<ContainerModel>? serviceResponse = await mapService.GetLocationDomain("google.com");

            if (serviceResponse.isSuccess)
            {

                Assert.True(serviceResponse.isSuccess);
            }

            else
            {
                Assert.Fail($"{serviceResponse.message}");
            }

        }

        [Fact]
        [TestPriority(6)]
        public async Task GetLocationDomain_False()
        {
            mapService = new();

            ServiceModel<ContainerModel>? serviceResponse = await mapService.GetLocationDomain("lifeisrough.iop");

            if (serviceResponse.isSuccess)
            {
                Assert.Fail($"{serviceResponse.message}");

            }

            else
            {
                Assert.False(serviceResponse.isSuccess);
            }

        }

        //[Fact]
        //[TestPriority(3)]
        //public async Task SetLocationTest()
        //{
        //    mapService = new();
        //}

        //public async Task setLazyLoad()
        //{
        //    mapJsModule = new();

        //    mapJsModule = new(await JS.InvokeAsync<IJSObjectReference>("import", "./js/map_function.js"));


        //}

    }
}
