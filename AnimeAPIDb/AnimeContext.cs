using AnimeAPIDb.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPIDb;

public class AnimeContext : DbContext
{
    public AnimeContext(DbContextOptions<AnimeContext> options) : base(options)
    {
    }

    public DbSet<Anime> Animes { get; set; }
}