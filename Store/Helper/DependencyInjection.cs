using Microsoft.EntityFrameworkCore;
using Store.Core.AutoMapping.Products;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Core;
using Store.Repository.Data.Contexts;
using Store.Repository.Repositories;
using Store.Repository;
using Store.Service.Services.Products;
using StackExchange.Redis;
using Store.Core.AutoMapping.Basket;
using Store.Service.Caches;

namespace Store.Helper
{ 
        //  Before Build :  
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependencyCallInProgram (this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContexts(configuration);
            services.AddServices();
            services.AddRepositories();
            services.AddUnitOfWork();
            services.AddAutoMapperProfile(configuration);
            services.AddSwagger(); 
            services.AddRedisService(configuration);

            return services;
        }

        private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, CacheService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        
        // sigleton >> 
        private static IServiceCollection AddRedisService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) => 
            {
                var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            return services;
        }

        private static IServiceCollection AddAutoMapperProfile(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            return services;

        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
