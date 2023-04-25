using AnimeAPIDb.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPIDb.Services;

public class TagService
{
    private readonly AnimeContext _db;
    private readonly ILogger<TagService> _logger;

    public TagService(ILogger<TagService> logger, AnimeContext db)
    {
        _logger = logger;
        _db = db;
    }
    
    public async Task<List<Tag>?> All()
    {
        return _db.Tags is null ? null : await _db.Tags.ToListAsync();
    }
    
    public async Task<Tag> Add(Tag tag)
    {
        await _db.Tags.AddAsync(tag);
        await _db.SaveChangesAsync();
        return tag;
    }
    
    public async Task<bool> Edit(Tag tag)
    {
        var tagFirst = _db.Tags.FirstOrDefault(x => x.Id == tag.Id);

        if (tagFirst is null)
            return false;

        _db.Entry(tagFirst).CurrentValues.SetValues(tag);
        await _db.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> Delete(int id)
    {
        var tag = await _db.Tags.FindAsync(id);
        if (tag is null)
            return false;
        
        _db.Tags.Remove(tag);
        await _db.SaveChangesAsync();
        return true;
    }
}