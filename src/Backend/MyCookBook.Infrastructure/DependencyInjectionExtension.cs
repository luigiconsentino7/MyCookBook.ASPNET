using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Domain.Repositories;
using MyCookBook.Domain.Repositories.User;
using MyCookBook.Infrastructure.DataAccess;
using MyCookBook.Infrastructure.DataAccess.Repositories;

namespace MyCookBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddDbContext_SqlServer(services);
            AddRepositories(services);
        }

        private static void AddDbContext_SqlServer(IServiceCollection services)
        {

            var connectionString = "Server=localhost;Database=mycookbookdb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

            services.AddDbContext<MyCookBookDbContext>(dbContextOption =>
            {
                dbContextOption.UseSqlServer(connectionString);
            });
            
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
