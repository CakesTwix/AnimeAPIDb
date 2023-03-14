using AnimeAPIDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AnimeAPIDb.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimeController : ControllerBase
{

    private readonly AnimeContext _db;
    private readonly ILogger<AnimeController> _logger;

    public AnimeController(ILogger<AnimeController> logger, AnimeContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    [Route("getAllAnime")]
    public async Task<IActionResult> GetAllAnimeAsync()
    {
        return Ok(await _db.Animes
            .Include(x => x.Tags)
            .Include(x => x.Seasons)
            .ThenInclude(x => x.Episodes)
            .ThenInclude(x =>x.Links)
            .ToListAsync()
        );
    }
    
    [HttpGet]
    [Route("getAnime")]
    public async Task<IActionResult> GetAnimeAsync(int? id, string? codename)
    {
        if (id.HasValue && !codename.IsNullOrEmpty())
        {
            return BadRequest("Only one parameter");
        }
        
        if(id.HasValue) return Ok(await _db.Animes.Include(x => x.Tags)
            .Include(x => x.Seasons)
            .ThenInclude(x => x.Episodes)
            .ThenInclude(x =>x.Links)
            .FirstOrDefaultAsync()
        );
        
        return Ok(await _db.Animes.Include(x => x.Tags)
            .Include(x => x.Seasons)
            .ThenInclude(x => x.Episodes)
            .ThenInclude(x =>x.Links)
            .Where(x => x.Codename == codename)
            .FirstOrDefaultAsync()
        );
    }

    [HttpPost]
    [Route("addAnime")]
    public async Task<IActionResult> AddAnimeAsync(Anime anime)
    {
        await _db.Animes.AddAsync(anime);
        await _db.SaveChangesAsync();
        return Ok(anime);
    }
}