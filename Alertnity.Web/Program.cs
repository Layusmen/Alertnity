using Alertnity.Web.Components;
using Alertnity.Web.Services;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using dotenv.net;


var builder = WebApplication.CreateBuilder(args);

//AppState registering
//builder.Services.AddSingleton<AppState>();
builder.Services.AddScoped<AppState>();
//CrimeCountingServices registering
builder.Services.AddScoped<CrimeCountingServices>();

// Add Blazorise services to the container.
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Load environment variables from .env file
DotEnv.Load();
app.UseStaticFiles();

// Store the Azure Maps key in configuration
var azureMapKey = Environment.GetEnvironmentVariable("AZURE_MAP_KEY");

// Make sure it's being loaded
if (string.IsNullOrEmpty(azureMapKey))
{
    Console.WriteLine("Azure Maps Key is NOT being loaded!");
}
else
{
    Console.WriteLine($"Azure Maps Key Loaded: {azureMapKey}");
}

//Console.WriteLine($"Azure Maps Key Loaded: {azureMapKey}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();