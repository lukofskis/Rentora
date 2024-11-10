using Microsoft.EntityFrameworkCore;
using Rentora.Data.Entities;
namespace Rentora.Data;

public class ForumDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Houses> Houses { get; set; }
    public DbSet<Rooms> Rooms { get; set; }
    public DbSet<Notes> Notes { get; set; }

    public ForumDbContext(IConfiguration configuration )
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
    }
    
}