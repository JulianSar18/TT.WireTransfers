using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using TT.WireTransfers.Domain.Interfaces;
using TT.WireTransfers.Infraestructure.Persistence.Data;
using TT.WireTransfers.Infraestructure.Persistence.Repositories;

namespace TT.WireTransfers.Infraestructure.Persistence
{
    public static class Extension
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var dbPassword = Environment.GetEnvironmentVariable("WIRETRANSFERSDB_PASSWORD");

            Debug.WriteLine($"WIRETRANSFERSDB_PASSWORD: {dbPassword}");


            var connectionString = configuration.GetConnectionString("WireTransfersDb");

            if (!string.IsNullOrEmpty(dbPassword))
            {
                connectionString = connectionString?.Replace("{WIRETRANSFERSDB_PASSWORD}", dbPassword);
            }

            Debug.WriteLine($"DB Connection String: {configuration.GetConnectionString("WireTransfersDb")}");
            services.AddDbContext<WalletDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<IMovementRepository, MovementRepository>();
        }
    }
}
