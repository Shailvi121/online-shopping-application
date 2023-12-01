using Microsoft.EntityFrameworkCore;
using Online_Shopping_Application.API.data;
using Online_Shopping_Application.API.Repository.Interface;
using System.Linq.Expressions;

namespace Online_Shopping_Application.API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FammsContext _context;
        public Repository(FammsContext context)
        {
            _context = context;
            
        }
        public virtual async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

        }
        public virtual async Task Update(int id, T entity)
        {
            var user = _context.Set<T>().FindAsync(id);
            if (user != null)
            {
                _context.Entry(user).CurrentValues.SetValues(entity);
                foreach (var property in _context.Entry(user).Properties)
                {
                    var currentValues = property.OriginalValue;
                    var propsedValues = property.CurrentValue;

                    if (propsedValues == null)
                    {
                        property.IsModified = false;
                    }
                    else if (!propsedValues.Equals(currentValues))
                    {
                        property.IsModified = true;
                    }

                }
                await _context.SaveChangesAsync();

            }

        }

        public virtual async Task<int> Delete(int id)
        {
            var user = await GetById(id);
            if (user == null)
            {
                return 0;
            }
            _context.Set<T>().Remove(user);
            return await _context.SaveChangesAsync();

        }

        public virtual async Task<T?> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate) ?? default(T);
        }



        public virtual async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(entity => EF.Property<int>(entity , "Id") == id); 

        }

        public virtual async Task<int> SaveChanges()
        {
            return _context.SaveChanges();
        }
        
        public async Task<IQueryable<T>> GetAll()
        {
            var entity = _context.Set<T>().AsQueryable();
            return await Task.FromResult(entity);
        }
    }

}
