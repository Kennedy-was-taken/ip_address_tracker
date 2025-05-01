using ip_address_tracker.Components;
using ip_address_tracker.Interfaces;
using ip_address_tracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Adding logger services
//builder.Logging.ClearProviders();
//builder.Logging.AddDebug();
//builder.Logging.AddConsole();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<IValidateInterface, ValidateService>();
builder.Services.AddTransient<MapService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
