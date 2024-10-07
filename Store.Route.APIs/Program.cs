
using Microsoft.EntityFrameworkCore;
using Store.Route.Core;
using Store.Route.Core.Mapping.Products;
using Store.Route.Core.Services.Contract;
using Store.Route.Repository;
using Store.Route.Repository.Data;
using Store.Route.Repository.Data.Contexts;
using Store.Route.Service.Services.Products;

namespace Store.Route.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")); 
            });

            builder.Services.AddScoped<IProductService , ProductService>();
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile()));

            var app = builder.Build();


            //StoreDbContext context = new StoreDbContext(); // I Created This Object But I need CLR Create It Instead Of Me
            //await context.Database.MigrateAsync(); //Update-Database\

            //So I  Use This |
            //               v

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>();
            var loggerFactory  = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);

            }
            catch (Exception ex )
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There Are Problems During Apply Migrations !");
            }




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

             app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
