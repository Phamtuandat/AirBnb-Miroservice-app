using Microsoft.AspNetCore.Mvc;

namespace PropertyApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LabelController : ControllerBase
{
      private readonly PropertyDbContext _context;
      public LabelController(PropertyDbContext propertyDbContext)
      {
            _context = propertyDbContext;
      }


      [HttpGet]
      public ActionResult GetAll()
      {
            var result = _context.Labels.ToList();
            return Ok(result);
      }

      [HttpPost]
      public async Task<ActionResult> CreateAsync(Label label)
      {

            var result = _context.Labels.Add(label);
            await _context.SaveChangesAsync();
            return Ok();
      }

      [HttpPut]
      public async Task<ActionResult> UpdateAsync(Label model)
      {
            var label = _context.Labels.FirstOrDefault(x => x.Id == model.Id);
            if (label != null)
            {
                  label.Name = model.Name;
                  label.ImgUrl = model.ImgUrl;
                  _context.Labels.Update(label);
                  await _context.SaveChangesAsync();
            }
            return NotFound("can't update label with id " + model.Id);
      }
      //delete label
      [HttpDelete]
      public async Task<ActionResult> DeleteAsync(string id)
      {
            var label = _context.Labels.FirstOrDefault(x => x.Id == id);
            if (label != null)
            {
                  _context.Labels.Remove(label);
                  await _context.SaveChangesAsync();
                  return Ok();
            }
            return NotFound("can't delete label with id " + id);
      }

}