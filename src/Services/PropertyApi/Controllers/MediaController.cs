using System.Data.Entity.Core;
using Microsoft.AspNetCore.Mvc;

namespace PropertyApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MediaController : ControllerBase
{

    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    // GET: api/Media
    [HttpGet]
    public ActionResult GetMedias()
    {

        return Ok(_mediaService.GetAll());
    }

    // GET: api/Media/5
    [HttpGet("{id}")]
    public ActionResult GetMedia(string id)
    {
        return Ok(_mediaService.GetById(id));
    }

    // PUT: api/Media/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMedia(string id, [FromForm] SaveImageDto model)
    {
        try
        {

            var media = await _mediaService.UpdateAsync(model, id);
            return Ok(media);
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }

    // POST: api/Media
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostMedia([FromForm] SaveImageDto media)
    {

        var newMedia = await _mediaService.SaveAsync(media);

        return CreatedAtAction("GetMedia", new { id = newMedia.Id }, newMedia);
    }

    // DELETE: api/Media/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedia(string id)
    {
        try
        {
            await _mediaService.DeleteAsync(id);
            return NoContent();

        }
        catch (ObjectNotFoundException)
        {

            return NotFound();
        }
    }


}
