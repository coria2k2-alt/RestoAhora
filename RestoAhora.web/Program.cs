using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Persistence;

using Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//-------------------------------------------------------------------------------
builder.Services.AddScoped<IMesaRepository, MesaRepository>();

builder.Services.AddScoped<IReservaMesaRepository, ReservaMesaRepository>();

builder.Services.AddScoped<ICategoriaProductoRepository, CategoriaProductoRepository>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
//------------------------------------------------------------------------------
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

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