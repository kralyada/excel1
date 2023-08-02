using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace excel
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<analysis> Analysis { get; set; }

        public ApplicationDbContext()
        {
            

            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.UseNpgsql("Host=10.241.224.71;Port=5432;Database=planning_dept_db;Username=postgres_10_241_224_71;Password=feDoz5Xreh");

        }

    }

    [Table("analysis")]
    public class analysis
    {
        [Key]
        public int? Id { get; set; }
        public string? Date_start { get; set; }
        public string? Change_start { get; set; }
        public string? Date_finish { get; set; }
        public string? Change_finish { get; set; }
        public string? period { get; set; }
        public string? condition { get; set; }
        public string? region { get; set; }
        public string? device { get; set; }
        public string? category { get; set; }
        public string? reason { get; set; }
        public string? coefficient { get; set; }
        public string? note { get; set; }

       
    }
}
