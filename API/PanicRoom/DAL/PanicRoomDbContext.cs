using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PanicRoom.Entities;
using System.Reflection;

namespace PanicRoom.DAL
{
    public class PanicRoomDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public PanicRoomDbContext(DbContextOptions<PanicRoomDbContext> options) : base(options) { }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<RepairProtocol> RepairProtocols { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);


            // One-to-One: Issue -> Ticket
            builder.Entity<Ticket>()
                .HasOne(t => t.Issue)
                .WithOne()
                .HasForeignKey<Ticket>(t => t.IssueID)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-One: Issue -> RepairProtocol
            builder.Entity<RepairProtocol>()
                .HasOne(rp => rp.Issue)
                .WithOne()
                .HasForeignKey<RepairProtocol>(rp => rp.IssueID)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-One: Issue -> Category
            builder.Entity<Issue>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(i => i.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-One: Issue -> User (Assigned User)
            builder.Entity<Issue>()
                .HasOne(i => i.UserAssigned)
                .WithMany() 
                .HasForeignKey(i => i.UserAssignedID)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-One: Issue -> User (Reported By)
            builder.Entity<Issue>()
                .HasOne(i => i.ReportedBy)
                .WithMany() 
                .HasForeignKey(i => i.ReportedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-One: Issue -> User (Resolved By)
            builder.Entity<Issue>()
                .HasOne(i => i.ResolvedBy)
                .WithMany() 
                .HasForeignKey(i => i.ResolvedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Enum conversions (optional, if you want to store enums as integers in the database)
            builder.Entity<Issue>()
                .Property(i => i.IssueStatusEnum)
                .HasConversion<int>();

            builder.Entity<Issue>()
                .Property(i => i.IssuePriorityEnum)
                .HasConversion<int>();

            // Default values for DateTime properties
            builder.Entity<Issue>()
                .Property(i => i.Created)
                .HasDefaultValueSql("NOW()");

            builder.Entity<Issue>()
                .Property(i => i.Updated)
                .HasDefaultValueSql("NOW()");





            // Seed Categories
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electrical" },
                new Category { Id = 2, Name = "Plumbing" },
                new Category { Id = 3, Name = "Carpentry" }
            );

            // Seed Issues
            builder.Entity<Issue>().HasData(
                new Issue
                {
                    Id = 1,
                    Title = "Broken Light",
                    Description = "Light in hallway is not working.",
                    CategoryID = 1,
                    IssueStatusEnum = IssueStatus.Open,
                    IssuePriorityEnum = IssuePriority.High,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                },
                new Issue
                {
                    Id = 2,
                    Title = "Leaking Pipe",
                    Description = "Pipe under kitchen sink is leaking.",
                    CategoryID = 2,
                    IssueStatusEnum = IssueStatus.Open,
                    IssuePriorityEnum = IssuePriority.Medium,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                }
            );
        }
    }
}
