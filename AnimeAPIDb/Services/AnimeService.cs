using AnimeAPIDb.Controllers;
using AnimeAPIDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AnimeAPIDb.Services
{
    public class AnimeService
    {
        private readonly AnimeContext _db;
        private readonly ILogger<AnimeService> _logger;

        public AnimeService(ILogger<AnimeService> logger, AnimeContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<List<Anime>?> GetAllAnimeAsync()
        {
            return _db.Animes is null ? null : await _db.Animes.Include(x => x.Tags)
                                   .Include(x => x.Seasons)
                                   .ThenInclude(x => x.Episodes)
                                   .ThenInclude(x => x.Links)
                                   .ToListAsync();
        }

        public async Task<Anime?> GetAnimeAsync(int? id, string? codename)
        {
            if (id.HasValue && !codename.IsNullOrEmpty())
            {
                return null;
            }

            if (id.HasValue) return await _db.Animes.Include(x => x.Tags)
                                                    .Include(x => x.Seasons)
                                                    .ThenInclude(x => x.Episodes)
                                                    .ThenInclude(x => x.Links)
                                                    .Where(x => x.Id == id)
                                                    .FirstOrDefaultAsync();

            return await _db.Animes.Include(x => x.Tags)
                                   .Include(x => x.Seasons)
                                   .ThenInclude(x => x.Episodes)
                                   .ThenInclude(x => x.Links)
                                   .Where(x => x.Codename == codename)
                                   .FirstOrDefaultAsync();
        }

        public async Task<Anime> AddAnimeAsync(Anime anime)
        {
            await _db.Animes.AddAsync(anime);
            await _db.SaveChangesAsync();
            return anime;
        }

        public async Task<bool> EditAnimeAsync(Anime anime)
        {
            var animeFirst = _db.Animes.FirstOrDefault(x => x.Id == anime.Id);

            if (animeFirst is null)
                return false;

            _db.Entry(animeFirst).CurrentValues.SetValues(anime);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAnimeAsync(int id)
        {
            var anime = await _db.Animes.FindAsync(id);
            if (anime is not null)
            {
                _db.Animes.Remove(anime);
            }

            await _db.SaveChangesAsync();
        }
    }
}
