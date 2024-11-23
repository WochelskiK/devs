using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PanicRoom.Entities;
using System.Reflection;

namespace PanicRoom.DAL
{
    public class PanicRoomDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public PanicRoomDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
