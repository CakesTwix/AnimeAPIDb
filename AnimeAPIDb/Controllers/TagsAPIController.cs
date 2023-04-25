using AnimeAPIDb.Models;
using AnimeAPIDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeAPIDb.Controllers;

[ApiController]
[Route("api/Tags")]
public class TagsAPIController : ControllerBase
{
    private readonly TagService _service;
    private readonly ILogger<TagsAPIController> _logger;

    public TagsAPIController(ILogger<TagsAPIController> logger, TagService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllTagsAsync()
    {
        return Ok(await _service.All());
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddTagAsync(Tag tag)
    {
        await _service.Add(tag);
        return Ok(tag);
    }

    [HttpPost]
    [Route("edit")]
    public async Task<IActionResult> EditTagAsync(Tag tag)
    {
        var result = await _service.Edit(tag);
        return result ? Ok() : BadRequest();
    }

    [HttpDelete]
    [Route(("del"))]
    public async Task<IActionResult> DelTagAsync(int id)
    {
        await _service.Delete(id);
        return Ok();
    }
}