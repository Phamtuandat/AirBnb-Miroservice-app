namespace PropertyApi.Services;

public interface IHostService
{
      Task<Householder> SigninAsync(SaveHouseholder householder);
      Task<Householder> UpdateAsync(SaveHouseholder saveHouseholder, string id);
      Task DeleteAsync(string id);
      ICollection<Householder> GetAll(int pageIndex = 0, int pageSize = 10);
      Householder Get(string id);
      ICollection<Householder> Find(Expression<Func<Householder, bool>> expression);

}