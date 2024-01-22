using Microsoft.EntityFrameworkCore;
using SGCU.Data;

namespace SGCU.Services;

public static class DbManagementService
{
    public static void MigrationInitialization(this IApplicationBuilder app) 
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var serviceDb = serviceScope.ServiceProvider.GetService<ApiDbContext>();

        serviceDb?.Database.Migrate();
    }
}