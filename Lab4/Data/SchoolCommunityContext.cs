using Microsoft.EntityFrameworkCore;
using Lab4.Models;

namespace Lab4.Data
{
    public class SchoolCommunityContext : DbContext
    {
        public SchoolCommunityContext(DbContextOptions<SchoolCommunityContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<CommunityMembership> CommunityMemberships { get; set; }
       // public DbSet<Advertisement> Advertisements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommunityMembership>().HasKey(c => new { c.StudentID, c.CommunityID });
            modelBuilder.Entity<CommunityMembership>().ToTable("memberships");
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
