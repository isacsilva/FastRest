using Core;
using Core.Service;
using Core.Server;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Service;

namespace FastRest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("FastRestConnection")
                ?? throw new InvalidOperationException("Connection string 'FastRestConnection' not found.");

            // Configura o contexto do banco com Pomelo + AutoDetect
            builder.Services.AddDbContext<FastRestContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString) // Auto detecta a versão do MySQL
                )
            );

            // Add Controllers + Views
            builder.Services.AddControllersWithViews();

            // Injeção de dependência dos serviços
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IRestaurantService, RestaurantService>();
            builder.Services.AddTransient<IOrdertableService, OrdertableService>();
            builder.Services.AddTransient<IOrderproductsService, OrderproductsService>();
            builder.Services.AddTransient<IMenucategoryService, MenucategoryService>();

            // Singleton do PeerServer para sockets
            builder.Services.AddSingleton<PeerServer>();

            // AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Inicia o servidor socket paralelo
            var peerServer = app.Services.GetRequiredService<PeerServer>();
            peerServer.Start(5050);

            // Pipeline padrão do ASP.NET Core
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
        }
    }
}