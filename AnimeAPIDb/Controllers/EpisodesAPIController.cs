using AnimeAPIDb.Models;
using AnimeAPIDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeAPIDb.Controllers;

[ApiController]
[Route("api/Episode")]
public class EpisodeAPIController : ControllerBase
{
    private readonly EpisodeService _service;
    private readonly ILogger<EpisodeAPIController> _logger;

    public EpisodeAPIController(ILogger<EpisodeAPIController> logger, EpisodeService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.All());
    }
    
    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(await _service.Get(id));
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddAsync(Episode episode, int animeId)
    {
        var result = await _service.Add(episode, animeId);
        if (result is null)
            return BadRequest();
        
        return Ok(episode);
    }

    [HttpPost]
    [Route("edit")]
    public async Task<IActionResult> EditAsync(Episode episode)
    {
        var result = await _service.Edit(episode);
        return result ? Ok() : BadRequest();
    }

    [HttpDelete]
    [Route(("del"))]
    public async Task<IActionResult> DelAsync(int id)
    {
        await _service.Delete(id);
        return Ok();
    }
}