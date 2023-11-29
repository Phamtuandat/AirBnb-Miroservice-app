using System.Data.Entity.Core;
using Microsoft.AspNetCore.Mvc;

namespace PropertyApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TypeController : ControllerBase
{
    private readonly ITypeService _typeService;
    public TypeController(ITypeService typeService)
    {
        _typeService = typeService;
    }

    // GET: api/Type
    [HttpGet]
    public ActionResult GetTypes()
    {
        return Ok(_typeService.GetAll());
    }

    // GET: api/Type/5
    [HttpGet("{id}")]
    public ActionResult GetPlaceType(string id)
    {
        var placeType = _typeService.GetById(id);
        if (placeType == null) return NotFound();
        return Ok(placeType);
    }

    // PUT: api/Type/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlaceType(string id, SaveTypeDto placeType)
    {
        try
        {
            var type = await _typeService.UpdateAsync(placeType, id);
            return Ok(type);
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }

    // POST: api/Type
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostPlaceType(SaveTypeDto placeType)
    {
        var type = await _typeService.CreateAsync(placeType);
        return Ok(type);
    }

    // DELETE: api/Type/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaceType(string id)
    {
        try
        {
            await _typeService.DeleteAsync(id);
            return NoContent();
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }

    }
}
