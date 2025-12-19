using backend.Migrations.Data;
using backend.Services.Desk;
using backend.Services.Reservation;
using backend.Services.User;
using Microsoft.EntityFrameworkCore;

namespace backend;

public static class Injections
{
    public static void LoadDependencies(this IServiceCollection services)
    {
        ConfigureDbContext(services, options => options.UseInMemoryDatabase("Desks"));
        AddServices(services);
    }
    
    private static void ConfigureDbContext(IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder)
    {
        services.AddDbContext<DeskDbContext>(optionsBuilder);
    }
    
    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IDeskService, DeskService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IUserService, UserService>();
    }
    
    
}