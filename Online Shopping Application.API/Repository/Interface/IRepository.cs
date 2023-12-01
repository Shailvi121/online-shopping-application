using System.Linq.Expressions;

namespace Online_Shopping_Application.API.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetById(int id);

        Task<T?> FindByAsync(Expression<Func<T, bool>> predicate);

        Task<IQueryable<T>> GetAll();

        Task Create(T entity);

        Task Update(int id, T entity);

        Task<int> Delete(int id);

        Task<int> SaveChanges();
    }

}
