namespace PropertyApi.Services;

public interface IReviewService
{
      Task<Review> CreateAsync(SaveReviewDto saveReviewDto);
      Task<Review> UpdateAsync(SaveReviewDto saveReviewDto, string id);
      Task DeleteAsync(string id);
      ICollection<Review> GetAll(int pageIdx = 0, int size = 20);
      ICollection<Review>? GetByPropertyId(string Id, int pageIdx = 0, int size = 20);
      ICollection<Review>? GetByUserId(string Id, int pageIdx = 0, int size = 20);
      Review? Get(string id);
}