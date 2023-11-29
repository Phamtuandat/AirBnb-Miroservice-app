using System.Data.Entity.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PropertyApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{

    private readonly IReviewService _reviewService;
    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // GET: api/Review
    [HttpGet]
    public ActionResult GetReviews(int page, int pageSize)
    {

        return Ok(_reviewService.GetAll(page - 1, pageSize));
    }

    // GET: api/Review/5
    [HttpGet("{id}")]
    public ActionResult GetReview(string id)
    {
        return Ok(_reviewService.Get(id));
    }

    // PUT: api/Review/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(string id, SaveReviewDto review)
    {
        try
        {
            await _reviewService.UpdateAsync(review, id);
            return NoContent();
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }

    }

    // POST: api/Review
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostReview(SaveReviewDto review)
    {
        return Ok(await _reviewService.CreateAsync(review));
    }

    // DELETE: api/Review/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(string id)
    {
        try
        {
            await _reviewService.DeleteAsync(id);
            return NoContent();

        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }
    }


}
