using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ip_address_tracker.Components.Pages
{
    public partial class Home : ComponentBase, IAsyncDisposable
    {
        private Lazy<IJSObjectReference> ExampleModule = new();

        private string ip_address = string.Empty;

        private string api_key = "at_vaFBN1fKp81LTOLx8xs2Ix5V0OOJ1";
        private string bare_url = "https://geo.ipify.org/api/v2/country,city?";

        [Inject]
        private IJSRuntime? JS { get; set; }

        protected override async Task OnInitializedAsync()
        {
             string data;
            Console.WriteLine("Life Life");

            data = await Task.FromResult("Async data loaded");

            Console.WriteLine();


        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JS is not null)
            {
                ExampleModule = new(await JS.InvokeAsync<IJSObjectReference>("import", "./js/map_function.js"));

                await ExampleModule.Value.InvokeVoidAsync("initMap");

            }

        }


        public void HandleButtonClick()
        {
            Console.WriteLine("sfef");
        }

        public async ValueTask DisposeAsync()
        {
            if (ExampleModule.IsValueCreated)
            {
                try
                {
                    await ExampleModule.Value.DisposeAsync();
                }

                catch (Exception)
                {

                }
                
            }
        }
    }
}
