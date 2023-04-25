using AnimeAPIDb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPIDb;

public class AnimeContext : IdentityDbContext
{
    public AnimeContext(DbContextOptions<AnimeContext> options) : base(options)
    {
    }

    public DbSet<Anime> Animes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Links> Links { get; set; }
}