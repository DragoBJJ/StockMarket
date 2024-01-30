using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SocialMediaApp.Models;

namespace SocialMediaApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stock {  get; set; } 
        
        public DbSet<Comment> Comments { get; set; }

            
    }
}
