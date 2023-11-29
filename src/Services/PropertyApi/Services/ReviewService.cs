
using System.Data.Entity.Core;

namespace PropertyApi.Services;

public class ReviewService : IReviewService
{
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;
      public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
      {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
      }
      public async Task<Review> CreateAsync(SaveReviewDto saveReviewDto)
      {
            try
            {
                  var newReview = _mapper.Map<SaveReviewDto, Review>(saveReviewDto);
                  var review = _unitOfWork.ReviewRepository.Add(newReview);
                  review.Id = Guid.NewGuid().ToString();
                  await _unitOfWork.SaveChangeAsync();
                  return review;
            }
            catch (Exception)
            {
                  throw;
            }
      }

      public async Task DeleteAsync(string id)
      {
            var review = _unitOfWork.ReviewRepository.Get(id) ?? throw new ObjectNotFoundException();
            try
            {
                  _unitOfWork.ReviewRepository.Delete(review);
                  await _unitOfWork.SaveChangeAsync();
            }
            catch (System.Exception)
            {
                  throw;
            }
      }

      public Review? Get(string id)
      {
            return _unitOfWork.ReviewRepository.Get(id);
      }

      public ICollection<Review> GetAll(int pageIdx = 0, int size = 20)
      {
            return _unitOfWork.ReviewRepository.All().Skip(pageIdx * size).Take(size).ToList();
      }

      public ICollection<Review> GetByPropertyId(string Id, int pageIdx = 0, int size = 20)
      {
            return _unitOfWork.ReviewRepository.All().Where(rv => rv.PropertyId == Id).Skip(pageIdx * size).Take(size).ToList();
      }

      public ICollection<Review> GetByUserId(string Id, int pageIdx = 0, int size = 20)
      {
            return _unitOfWork.ReviewRepository.All().Where(rv => rv.UserId == Id).Skip(pageIdx * size).Take(size).ToList();
      }

      public async Task<Review> UpdateAsync(SaveReviewDto saveReviewDto, string id)
      {
            var review = _unitOfWork.ReviewRepository.Get(id) ?? throw new ObjectNotFoundException();
            try
            {
                  review.LastComment = review.Comment;
                  review.Comment = saveReviewDto.Comment;
                  review.UpdateAt = DateTime.Now.ToString();
                  var updateRv = _unitOfWork.ReviewRepository.Update(review);
                  await _unitOfWork.SaveChangeAsync();
                  return updateRv;
            }
            catch (System.Exception)
            {
                  throw;
            }
      }
}