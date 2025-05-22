using Microsoft.EntityFrameworkCore;
using VisitorManagement.Models;


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VisitLog> VisitLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisitLog>()
                .HasOne(v => v.Visitor)
                .WithMany()
                .HasForeignKey(v => v.VisitorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VisitLog>()
                .HasOne(v => v.HostEmployee)
                .WithMany()
                .HasForeignKey(v => v.HostEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    
}
