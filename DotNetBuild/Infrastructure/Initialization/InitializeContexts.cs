using Microsoft.EntityFrameworkCore;
using  DotNetBuild.Domain.Models;

namespace DotNetBuild.Infrastructure.Initialization
{
    public class InitializeContext : DbContext
    {
        public InitializeContext(DbContextOptions<InitializeContext> options) : base(options)
        {

        }
        public DbSet<Sample> Sample { get; set; }
    }
}
