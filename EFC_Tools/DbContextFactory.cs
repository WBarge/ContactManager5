using System.IO;
using Contact.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EFC_Tools
{
    public class DbContextFactory: IDesignTimeDbContextFactory<ContactDbContext>
    {
        public ContactDbContext CreateDbContext(string[] args)
        {
            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<ContactDbContext>();

            var conStr = conf["ConnectionString"];

            dbContextBuilder.UseSqlServer(conStr);

            return new ContactDbContext(dbContextBuilder.Options);
        }
    }
}
