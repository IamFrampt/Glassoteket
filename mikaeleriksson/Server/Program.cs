


using Microsoft.EntityFrameworkCore;
using mikaeleriksson.Server.Data.Domain;
using mikaeleriksson.Server.Extensions;
using mikaeleriksson.Server.Services;
using mikaeleriksson.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GlassotekDbContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("GlassoteketDB");
	options.UseSqlServer(connectionString);
});




// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();
app.UseAuthentication();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();


app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");
app.MapAuthEndPoints();

app.MapRazorPages();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});


app.MapFallbackToFile("index.html");


app.Run();
