using Search.Web.Code;
using Search.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// configure options
// normally I would group all of the IOptions together, and the DI classes/interfaces in separate methods.
// configure setting classes as IOption
builder.Services.Configure<SearchWebApiSettings>(
    builder.Configuration.GetSection(nameof(SearchWebApiSettings)));
// configure scoped instances
builder.Services.AddScoped<CameraLocationsDividedRetriever>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
