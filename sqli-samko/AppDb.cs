using Microsoft.EntityFrameworkCore;
using sqli_samko.Models;

namespace sqli_samko
{
    public class AppDb:DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) :base(options){ }
        public DbSet<Cars> Carss { get; set; }
    }
}
