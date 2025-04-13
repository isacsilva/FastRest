using Core;
using Core.Service;
using Core.Server; 
using Microsoft.EntityFrameworkCore;
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

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<FastRestContext>(
                options => options.UseMySQL(connectionString));

            // Injeção de dependência dos Services
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IRestaurantService, RestaurantService>();
            builder.Services.AddTransient<IOrdertableService, OrdertableService>();
            builder.Services.AddTransient<IOrderproductsService, OrderproductsService>();
            builder.Services.AddTransient<IMenucategoryService, MenucategoryService>();

            // Injeção de dependência dos mappers
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Iniciar o PeerServer em segundo plano
            PeerServer.Start(); // ← Aqui é onde o servidor socket começa a escutar

            // Configure o pipeline HTTP
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