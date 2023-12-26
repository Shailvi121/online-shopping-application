
namespace Online_Shopping_Application.API.Repository
{
    public class CategoryRepository : Repository<Category>, ICategory
    {

        private readonly FammsContext _context;
        private readonly IMemoryCache _cache;
        private readonly CacheManager<Category> _cacheManager;

        public CategoryRepository(FammsContext context, IMemoryCache cache, CacheManager<Category> cacheManager) : base(context)
        {

            _context = context;
            _cache = cache;
            _cacheManager = cacheManager;

        }
        public override async Task<IQueryable<Category>> GetAll()
        {

            var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey);
            if (cachedCategory != null)
            {
                return cachedCategory.AsQueryable();
            }
            var entity = _context.Categories.ToList();
            _cacheManager.Set(Constants.CacheKeys.CategoryKey, entity);
            return entity.AsQueryable();

        }

        public override async Task<Category?> GetById(int id)
        {
            var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey);
            var cachedEntity = cachedCategory.FirstOrDefault(e => e.Id == id);
            if (cachedEntity != null)
            {
                return cachedEntity;
            }
            var entity = _context.Categories.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                cachedCategory ??= new List<Category>();
                cachedCategory.Add(entity);

                _cacheManager.Set(Constants.CacheKeys.CategoryKey, cachedCategory);

            }
            return entity;
        }
        public override async Task<Category?> FindByAsync(Expression<Func<Category, bool>> predicate)
        {


            var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey);
            var cachedEntity = cachedCategory.FirstOrDefault(predicate.Compile());

            if (cachedEntity != null)
            {
                return cachedEntity;
            }


            var entity = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);

            if (entity != null)
            {
                cachedCategory ??= new List<Category>();
                cachedCategory.Add(entity);

                _cache.Set(Constants.CacheKeys.CategoryKey, cachedCategory);

            }

            return entity;
        }
        public override async Task Create(Category category)
        {

            await _context.Categories.AddAsync(category);


            //await _context.SaveChangesAsync();

            var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey);
            if (cachedCategory == null)
            {

                cachedCategory.Add(category);


                _cacheManager.Set(Constants.CacheKeys.CategoryKey, cachedCategory);

            }
            else
            {

               // _cacheManager.Set(Constants.CacheKeys.CategoryKey, new List<Category> { category }, new MemoryCacheEntryOptions);


            }
        }

        public override async Task Update(int id, Category entity)
        {
            // Find the entity in the database context asynchronously
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                // Update the entity properties with the new values
                _context.Entry(category).CurrentValues.SetValues(entity);

                foreach (var property in _context.Entry(category).Properties)
                {
                    var currentValues = property.OriginalValue;
                    var proposedValues = property.CurrentValue;

                    if (proposedValues == null)
                    {
                        property.IsModified = false;
                    }
                    else if (!proposedValues.Equals(currentValues))
                    {
                        property.IsModified = true;



                    }
                }

                await _context.SaveChangesAsync();


                var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey);
                if (cachedCategory != null)
                {
                    var index = cachedCategory.FindIndex(g => g.Id == id);
                    if (index != -1)
                    {
                        cachedCategory[index] = category;

                        _cacheManager.Set(Constants.CacheKeys.CategoryKey, cachedCategory);

                    }
                }
            }
        }

        public override async Task<int> Delete(int id)
        {
            var user = await GetById(id);
            if (user == null)
            {
                return 0;
            }
            _context.Categories.Remove(user);
            var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey);
            if (cachedCategory!= null)  
            {
                cachedCategory.Remove(user);
                _cacheManager.Set(Constants.CacheKeys.CategoryKey, cachedCategory);
               
            }

            return await _context.SaveChangesAsync();

        }


    }
}



