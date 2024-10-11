using Fridayfrietday;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DBContext>();
builder.Services.AddDistributedMemoryCache(); // Enable session storage
builder.Services.AddSession(); // Add session middleware
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ShoppingCartService>(); 

var app = builder.Build();

app.UseHttpsRedirection();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var cultureInfo = new CultureInfo("nl-NL");
cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();
app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
