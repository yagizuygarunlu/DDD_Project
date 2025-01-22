using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DDD_Project.Application.Common.Interfaces;
using DDD_Project.Domain.Aggregates.TaskAggregate;
using DDD_Project.Infrastructure.Persistence;
using DDD_Project.Infrastructure.Persistence.Repositories;

namespace DDD_Project.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
} 