using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppTresCamadas.Application.Data;
using AppTresCamadas.Data.Context;
using AppTresCamadas.Business.Interfaces;
using AppTresCamadas.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var newConnectionString = builder.Configuration.GetConnectionString("NewConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(newConnectionString));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(newConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();