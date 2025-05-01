using ip_address_tracker.Interfaces;
using ip_address_tracker.Model;
using ip_address_tracker.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Diagnostics;

namespace ip_address_tracker.Components.Pages
{
    public partial class Home : ComponentBase, IAsyncDisposable
    {
        [Inject]
        private IJSRuntime? JS { get; set; }

        private readonly IValidateInterface validate;
        private readonly MapService mapService;

        // binding value
        private string search_value = string.Empty;
        private string ip_address = string.Empty;
        private string location = string.Empty;
        private string timezone = string.Empty;
        private string isp = string.Empty;



        public Home()
        {
            validate = new ValidateService();
            mapService = new MapService();
        }

        private Lazy<IJSObjectReference>? mapJsModule = new();


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JS is not null)
            {
                mapJsModule = new(await JS.InvokeAsync<IJSObjectReference>("import", "./js/map_function.js"));

                await mapJsModule.Value.InvokeVoidAsync("initMap");

                string publicIP = await mapService.GetPublicIP();

                ServiceModel<ContainerModel> infoFromIP = await GetLocationByIP(publicIP);

                await results(infoFromIP);

                StateHasChanged();

            }

        }

        // determines if its an IP address or domain name
        public async Task HandleButtonClick()
        {
            if (!string.IsNullOrEmpty(this.search_value))
            {
                ServiceModel<ContainerModel> container = new();
                bool isIPAddress = validate.isIPAddress(this.search_value);

                if (isIPAddress)
                {
                    container = await GetLocationByIP(search_value);
                }

                else
                {
                    container = await GetLocationByDomainName(search_value);
                }

                await results(container);

            }
        }

        private async Task results(ServiceModel<ContainerModel> info)
        {
            if (info != null && info.isSuccess)
            {
                await SetLocation(info.Data.location.lat, info.Data.location.lng, mapJsModule);
                displayLocation(info);

            }

            // come back
            else
            {
                await setErrorMessage(info.message);
            }
        }

        private async Task<ServiceModel<ContainerModel>> GetLocationByIP(string search_value)
        {

            return await mapService.GetLocationIP(search_value);
        }

        private async Task<ServiceModel<ContainerModel>> GetLocationByDomainName(string search_value)
        {

            return await mapService.GetLocationDomain(search_value);
        }

        private void displayLocation(ServiceModel<ContainerModel> info)
        {

            if (info.Data != null)
            {
                ip_address = info.Data.ip;
                location = $"{info.Data.location?.country}, {info.Data.location?.region}";
                timezone = $"UTC {info.Data.location?.timezone}";
                if (info.Data.isp is null || info.Data.isp.Equals("") || info.Data.isp.Equals(" "))
                {
                    isp = "No ISP";
                }
                else
                {
                    isp = info.Data.isp;
                }
                

            }
        }

        // Javascript methods
        private async Task setErrorMessage(string message)
        {
            await mapJsModule.Value.InvokeVoidAsync("error", message);
        }

        private async Task SetLocation(double lat, double lng, Lazy<IJSObjectReference> mapJsModule)
        {
            await mapJsModule.Value.InvokeVoidAsync("coordinates", lat, lng);
            Debug.WriteLine("It made it here");
        }

        public async ValueTask DisposeAsync()
        {
            if (mapJsModule.IsValueCreated)
            {
                try
                {
                    await mapJsModule.Value.DisposeAsync();
                }

                catch (Exception)
                {

                }

            }
        }
    }
}
