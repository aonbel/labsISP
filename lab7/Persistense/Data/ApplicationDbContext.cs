using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    private DbSet<Song> Songs { get; set; }
    private DbSet<Artist> Artists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>().OwnsOne<SongData>(s => s.Data);
    }
}