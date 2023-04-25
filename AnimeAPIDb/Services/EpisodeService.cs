using AnimeAPIDb.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPIDb.Services;

public class EpisodeService
{
    private readonly AnimeContext _db;
    private readonly ILogger<AnimeContext> _logger;

    public EpisodeService(ILogger<AnimeContext> logger, AnimeContext db)
    {
        _logger = logger;
        _db = db;
    }
    
    public async Task<List<Episode>?> All()
    {
        return _db.Episodes is null ? null : await _db.Episodes.ToListAsync();
    }
    
    public async Task<Episode>? Get(int id)
    {
        return await _db.Episodes.Where(episode => episode.Id == id).Include(x => x.Links).FirstAsync();
    }
    
    public async Task<Episode>? Add(Episode episode, int animeID)
    {
        var anime = await _db.Animes.Where(anime => anime.Id == animeID).FirstOrDefaultAsync();
        if (anime is null)
            return null;
        
        anime.Episodes.Add(episode);
        await _db.SaveChangesAsync();
        return episode;
    }
    
    public async Task<Episode> Get(Episode episode)
    {
        await _db.Episodes.AddAsync(episode);
        await _db.SaveChangesAsync();
        return episode;
    }
    
    public async Task<bool> Edit(Episode episode)
    {
        var First = _db.Episodes.FirstOrDefault(x => x.Id == episode.Id);

        if (First is null)
            return false;

        _db.Entry(First).CurrentValues.SetValues(episode);
        await _db.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> Delete(int id)
    {
        var episode = await _db.Episodes.FindAsync(id);
        if (episode is null)
            return false;
        
        _db.Episodes.Remove(episode);
        await _db.SaveChangesAsync();
        return true;
    }
}