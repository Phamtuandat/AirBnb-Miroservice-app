using System.Data.Entity.Core;
using Microsoft.AspNetCore.Mvc;

namespace PropertyApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    // GET: api/Property
    [HttpGet]
    public ActionResult GetProperties(int page = 1, int pageSize = 10)
    {

        return Ok(_propertyService.GetAll(page - 1, pageSize));
    }

    // GET: api/Property/5
    [HttpGet("{id}")]
    public ActionResult GetProperty(string id)
    {
        return Ok(_propertyService.GetById(id));
    }

    // PUT: api/Property/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProperty(string id, SavePropertyDto property)
    {
        try
        {

            var updated = await _propertyService.UpdateAsync(property, id);
            return Ok(updated);
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }

    // POST: api/Property
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostProperty(SavePropertyDto property)
    {
        return Ok(await _propertyService.CreateAsync(property));
    }

    // DELETE: api/Property/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(string id)
    {
        try
        {
            await _propertyService.DeleteAsync(id);

            return NoContent();
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }


}
