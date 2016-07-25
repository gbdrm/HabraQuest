using Microsoft.EntityFrameworkCore;

namespace HabraQuest.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }


        public DbSet<Player> Players { get; set; }
    }
}
