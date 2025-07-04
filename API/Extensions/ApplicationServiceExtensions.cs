using API;
using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace API.Extensions;
public static class ApplicationServiceExtensions {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {
        services.AddControllers();
        services.AddDbContext<DataContext>(Opt =>
        {
            Opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}