using Microsoft.EntityFrameworkCore;
using Store.Core.AutoMapping.Products;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Repository.Data.Contexts;
using Store.Repository.Repositories;
using Store.Service.Services.Products;
using Store.Repository.Data;
using Store.Repository.Identity.Contexts;
using Store.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.Identity;

namespace Store.Helper
{
        //  After Build :  
    public static class ConfigureMiddleware
    {
        // Method to handle database migration and seeding
        public static async Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreDbContext>();  // ask CLR to create  object StoreDBContext
            var contextIdentity = services.GetRequiredService<StoreIdentityDBContext>(); // ask CLR to create  object StoreIdentityDBContext
            var userManager = services.GetRequiredService<UserManager<AppUser>>(); // ask CLR to create  object StoreIdentityDBContext

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);

                await contextIdentity.Database.MigrateAsync();
                await StoreIdentityDBContextSeed.SeedAppUserAsync(userManager);
            }
            catch (Exception ex)
            { 
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There was a problem applying migrations!");
            }
            // Method to configure middleware
            // Check for development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            // Middleware to serve static files
            app.UseStaticFiles();

            // Middleware for HTTPS redirection
            app.UseHttpsRedirection();

            // Middleware for authintication 
            app.UseAuthentication();

            // Middleware for authorization
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            return app;
        }
    }
}

 