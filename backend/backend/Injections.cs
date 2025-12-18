using backend.Deps.Password;
using backend.Migrations.Data;
using backend.Services.Desk;
using Microsoft.EntityFrameworkCore;

namespace backend;

public static class Injections
{
    public static void LoadDependencies(this IServiceCollection services)
    {
        ConfigureDbContext(services, options => options.UseInMemoryDatabase("Desks"));
        AddServices(services);
        AddUtilities(services);
    }
    
    private static void ConfigureDbContext(IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder)
    {
        services.AddDbContext<DeskDbContext>(optionsBuilder);
    }
    
    private static void AddUtilities(IServiceCollection services)
    {
        services.AddTransient<IPasswordEncoder, BCryptPasswordEncoder>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IDeskService, DeskService>();
    }
    
    
}