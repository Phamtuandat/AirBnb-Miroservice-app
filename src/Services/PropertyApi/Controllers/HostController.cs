using System.Data.Entity.Core;
using Microsoft.AspNetCore.Mvc;

namespace PropertyApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HostController : ControllerBase
{
    private readonly IHostService _hostService;

    public HostController(IHostService hostService)
    {
        _hostService = hostService;
    }

    // GET: api/Host
    [HttpGet]
    public ActionResult GetHosts(int pageIndex = 0, int pageSize = 10)
    {

        return Ok(_hostService.GetAll(0, 10).ToList());
    }

    // GET: api/Host/5
    [HttpGet("{id}")]
    public ActionResult GetHouseholder(string id)
    {
        var householder = _hostService.Get(id);
        return Ok(householder);
    }

    // PUT: api/Host/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHouseholder(string id, SaveHouseholder householder)
    {
        try
        {
            var updated = await _hostService.UpdateAsync(householder, id);
            return Ok(updated);

        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }

    // POST: api/Host
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostHouseholder(SaveHouseholder householder)
    {

        var newHost = await _hostService.SigninAsync(householder);

        return Ok(newHost);
    }

    // DELETE: api/Host/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHouseholder(string id)
    {
        try
        {

            await _hostService.DeleteAsync(id);
            return NoContent();
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }

}
