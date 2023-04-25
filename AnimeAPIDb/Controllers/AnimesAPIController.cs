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
    [Route("all")]
    public async Task<IActionResult> GetAllAnimesAsync()
    {
        return Ok(await _service.GetAllAnimeAsync());
    }
    
    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetAnimeAsync(int? id, string? codename)
    {
        return Ok(await _service.GetAnimeAsync(id, codename));
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddAnimeAsync(Anime anime)
    {
        await _service.AddAnimeAsync(anime);
        return Ok(anime);
    }

    [HttpPost]
    [Route("edit")]
    public async Task<IActionResult> EditAnimeAsync(Anime anime)
    {
        var result = await _service.EditAnimeAsync(anime);
        return result ? Ok() : BadRequest();
    }

    [HttpDelete]
    [Route(("del"))]
    public async Task<IActionResult> DelAnimeAsync(int id)
    {
        await _service.DeleteAnimeAsync(id);
        return Ok();
    }
}