using Microsoft.EntityFrameworkCore;
using nitipApi.Models;

namespace nitipApi.DataAccess
{
    public class NitipContext : DbContext
    {
        public NitipContext(DbContextOptions<NitipContext> options)
            : base(options)
        {
        }

        public DbSet<NitipItem> NitipItems { get; set; }

    }
}