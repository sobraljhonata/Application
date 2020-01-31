using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Application.Infra.CrossCuting.Identity.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityLocalDbContext>
    {
        public IdentityLocalDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<IdentityLocalDbContext>();

            var connectionString = configuration.GetConnectionString("IdentityConnection");

            builder.UseSqlServer(connectionString);

            return new IdentityLocalDbContext(builder.Options);
        }
    }
}