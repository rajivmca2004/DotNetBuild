using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using  DotNetBuild.Domain.Models;
using System;
using System.Linq;

namespace DotNetBuild.Infrastructure.Initialization
{
    public static class MigrationManager
{
    public static IWebHost InitiateSampleDataMigration(this IWebHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<InitializeContext>())
            {
                try
                {
                    var db = scope.ServiceProvider.GetService<InitializeContext>();
                    db.Database.Migrate();

                    if (!db.Sample.Any(x => x.Id == 1))
                    {
                        AddData<Sample>(db, new Sample() { Id = 1, Name = "Vikash Agarwal", Details = "Technical Architect - .Net" });
                    }
                    if (!db.Sample.Any(x => x.Id == 2))
                    {
                        AddData<Sample>(db, new Sample() { Id = 2, Name = "Mallesh Thota", Details = "Technical Lead - .Net" });
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }
        return host;
    }

    private static void AddData<TData>(DbContext db, object item) where TData : class
    {
        db.Entry(item).State = EntityState.Added;
    }

}
}
