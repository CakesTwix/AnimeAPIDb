using AnimeAPIDb.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPIDb;

public class AnimeContext : DbContext
{
    public AnimeContext(DbContextOptions<AnimeContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    public DbSet<Anime> Animes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episodes> Episodes { get; set; }
    public DbSet<Links> Links { get; set; }
}