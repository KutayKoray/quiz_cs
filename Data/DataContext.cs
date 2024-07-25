using System.Diagnostics.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Api
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Question> Questions => Set<Question>();


        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Data Source=sqlite.db")
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Question>().HasKey(q => q.soru_id);
        }
    }
}
