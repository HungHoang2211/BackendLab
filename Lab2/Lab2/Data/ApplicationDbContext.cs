using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lab2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
        options) : base(options){}
        public DbSet<GameLevel> GameLevels { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
