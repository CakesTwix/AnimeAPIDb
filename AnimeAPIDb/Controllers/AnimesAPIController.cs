using AnimeAPIDb.Models;
using AnimeAPIDb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnimeAPIDb.Controllers;

[ApiController]
[Route("api/Animes")]
public class AnimesAPIController : ControllerBase
{
    private readonly AnimeService _service;
    private readonly ILogger<AnimesAPIController> _logger;

    public AnimesAPIController(ILogger<AnimesAPIController> logger, AnimeService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Route("getAllAnime")]
    public async Task<IActionResult> GetAllAnimeAsync()
    {
        return Ok(await _service.GetAllAnimeAsync());
    }

    [HttpGet]
    [Route("getAnime")]
    public async Task<IActionResult> GetAnimeAsync(int? id, string? codename)
    {
        if (id.HasValue && !codename.IsNullOrEmpty())
        {
            return BadRequest("Only one parameter");
        }

        if (id.HasValue) return Ok(await _service.GetAnimeAsync(id, null));

        return Ok(await _service.GetAnimeAsync(null, codename));
    }

    [HttpPost]
    [Route("addAnime")]
    public async Task<IActionResult> AddAnimeAsync(Anime anime)
    {
        await _service.AddAnimeAsync(anime);
        return Ok(anime);
    }

    [HttpPost]
    [Route("editAnime")]
    public async Task<IActionResult> EditAnimeAsync(Anime anime)
    {
        var result = await _service.EditAnimeAsync(anime);
        return result ? Ok() : BadRequest();
    }
}