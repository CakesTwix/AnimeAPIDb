using AnimeAPIDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [Route("getAnime")]
    public async Task<IActionResult> GetAnimeAsync()
    {
        return Ok(await _db.Animes.Include(x => x.Tags).ToListAsync());
    }
}