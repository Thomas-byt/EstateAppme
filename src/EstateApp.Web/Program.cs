using EstateApp.Data.DatabaseContexts.ApplicationDbContext;
using EstateApp.Data.DatabaseContexts.AuthenticationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
/* builder.Services.AddDbContextPool<AuthenticationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthenticationConnection"),
sqlServerOptions => 
{
    sqlServerOptions.MigrationsAssembly("EstateApp.Data");
}
));

builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnection"), 
sqlServerOptions => {
    sqlServerOptions.MigrationsAssembly("EstateApp.Data");
})); */

builder.Services.AddDbContextPool<AuthenticationDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("AuthenticationConnection"), 
mysqlOptions => {
    mysqlOptions.MigrationsAssembly("EstateApp.Data");
}
));

builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("ApplicationConnection"), 
mysqlOptions => 
{
    mysqlOptions.MigrationsAssembly("EstateApp.Data");
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
