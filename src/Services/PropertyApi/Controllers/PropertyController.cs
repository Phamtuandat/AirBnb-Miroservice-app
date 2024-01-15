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

    // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
    [HttpGet]
    [Route("items")]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<Property>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Property>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PaginatedItemsViewModel<PropertyViewModel>> GetProperties([FromQuery] string? sortBy = "Title", [FromQuery] int? lt = 100000, [FromQuery] int? gt = 0, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 12)
    {
        if (pageIndex < 0) return BadRequest();
        var result = _propertyService.GetAll(pageIndex, pageSize, sortBy);
        return Ok(result);
    }


    [HttpGet]
    [Route("items/{id}")]
    public ActionResult GetProperty(string id)
    {
        var property = _propertyService.GetById(id);
        if (property == null) return NotFound();
        return Ok(property);
    }

    // GET api/v1/[controller]/items/withname/samplename[?pageSize=3&pageIndex=10]
    [HttpGet]
    [Route("items/withname/{name:minlength(1)}")]
    public ActionResult<PaginatedItemsViewModel<PropertyViewModel>> SearchByName(string name, [FromQuery] int pageSize = 12, [FromQuery] int pageIndex = 0)
    {
        return _propertyService.Find(x => x.Title == name, pageIndex, pageSize);
    }
    [Route("items/search")]
    [HttpGet]
    public ActionResult<PaginatedItemsViewModel<PropertyViewModel>> GetProperties([FromQuery] string? country = "", [FromQuery] string? city = "", [FromQuery] int pageSize = 12, [FromQuery] int pageIndex = 0, [FromQuery] string? sortBy = "Title", [FromQuery] int? lt = 100000, [FromQuery] int? gt = 0)
    {
        var result = _propertyService.Find(x => x.Country.ToLower() == country.ToLower() || x.City.ToLower() == city.ToLower(), pageIndex, pageSize);
        return result;
    }
    // GET api/v1/[controller]/items/type/1/brand[?pageSize=3&pageIndex=10]
    [HttpGet]
    [Route("items/type/{typeId?}/Host/{HostId?}")]
    public ActionResult<PaginatedItemsViewModel<PropertyViewModel>> GetProperties(string? typeId = null, string? hostId = null, [FromQuery] int pageSize = 12, [FromQuery] int pageIndex = 0)
    {
        return _propertyService.Find(x => x.TypeId == typeId && x.HostId == hostId, pageIndex, pageSize);
    }
    // GET api/v1/[controller]/items/type/all/brand[?pageSize=3&pageIndex=10]
    [HttpGet]
    [Route("items/host/{hostId?}")]
    public ActionResult<PaginatedItemsViewModel<PropertyViewModel>> GetProperties(string? hostId, [FromQuery] int pageSize = 12, [FromQuery] int pageIndex = 0)
    {

        return _propertyService.Find(x => x.HostId == hostId, pageIndex, pageSize);
    }

    [HttpGet]
    [Route("items/label/{labelId}")]
    public ActionResult Label(string labelId, [FromQuery] int pageSize = 12, [FromQuery] int pageIndex = 0)
    {
        var result = _propertyService.GetPropertiesByLabel(labelId, pageIndex, pageSize);
        return Ok(result);
    }

    [HttpGet]
    [Route("items/type/{typeId}")]
    public ActionResult<PaginatedItemsViewModel<PropertyViewModel>> GetByTypeId(string? typeId, [FromQuery] int pageSize = 12, [FromQuery] int pageIndex = 0)
    {

        return _propertyService.Find(x => x.TypeId == typeId, pageIndex, pageSize);
    }

    [HttpPut]
    [Route("{id}")]
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
    [Route("items")]
    public async Task<ActionResult> PostProperty(SavePropertyDto property)
    {
        return Ok(await _propertyService.CreateAsync(property));
    }

    // DELETE: api/Property/5
    [HttpDelete]
    [Route("{id}")]
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
