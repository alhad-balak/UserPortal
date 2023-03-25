using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyDetails.Areas.Identity.Data;
using MyDetails.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MyDetailsDBContextConnection") ?? throw new InvalidOperationException("Connection string 'MyDetailsDBContextConnection' not found.");

builder.Services.AddDbContext<MyDetailsDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<MyDetailsUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<MyDetailsDBContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
