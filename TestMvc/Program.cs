using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestMvc.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TestMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestMvcContext") ?? throw new InvalidOperationException("Connection string 'TestMvcContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
