
namespace Online_Shopping_Application.API.Repository
{
    public class CategoryRepository : Repository<Category>, ICategory
    {

        private readonly FammsContext _context;
        private readonly IMemoryCache _cache;

        public CategoryRepository(FammsContext context, IMemoryCache cache) : base(context)
        {

            _context = context;
            _cache = cache;

        }
        public override async Task<IQueryable<Category>> GetAll()
        {

            if (_cache.TryGetValue("CategoryAllData", out IQueryable<Category> cachedData))
            {
                return cachedData;
            }


            var entity = _context.Set<Category>().AsQueryable();


            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(3)
            };

            _cache.Set("CategoryAllData", entity, cacheEntryOptions);

            return await Task.FromResult(entity);
        }

        public override async Task<Category?> GetById(int id)
        {

            if (_cache.TryGetValue("CategoryCache", out List<Category> cachedData))
            {
                var cachedEntity = cachedData.FirstOrDefault(entity => entity.Id == id);

                if (cachedEntity != null)
                {

                    return cachedEntity;

                }

            }
            var entity = _context.Categories.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                cachedData ??= new List<Category>();
                cachedData.Add(entity);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                };

                _cache.Set("CategoryCache", cachedData, cacheEntryOptions);

            }
            return entity;
        }
        public override async Task<Category?> FindByAsync(Expression<Func<Category, bool>> predicate)
        {
          

            if (_cache.TryGetValue("CategoriesCache", out List<Category> cachedCategories))
            {
                var cachedEntity = cachedCategories.FirstOrDefault(predicate.Compile());

                if (cachedEntity != null)
                {
                    return cachedEntity;
                }
            }

            var entity = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);

            if (entity != null)
            {
                cachedCategories ??= new List<Category>();
                cachedCategories.Add(entity);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                };

                _cache.Set("CategoryCache", cachedCategories, cacheEntryOptions);

            }

            return entity;
        }
        public override async Task Create(Category category)
        {

            await _context.Categories.AddAsync(category);


            await _context.SaveChangesAsync();


            if (_cache.TryGetValue("CategoriesCache", out List<Category> cachedCategories))
            {

                cachedCategories.Add(category);


                _cache.Set("CategoriesCache", cachedCategories, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(1)
                });
            }
            else
            {

                _cache.Set("CategoriesCache", new List<Category> { category }, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(1)
                });
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


                if (_cache.TryGetValue("CategoriesCache", out List<Category> cachedCategories))
                {
                    var index = cachedCategories.FindIndex(g => g.Id == id);
                    if (index != -1)
                    {
                        cachedCategories[index] = category;

                        _cache.Set("CategoriesCache", cachedCategories, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                            SlidingExpiration = TimeSpan.FromMinutes(1)
                        });
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
            if (_cache.TryGetValue("CategoriesCache", out List<Category> cachedCategories))
            {
                cachedCategories.Remove(user);
                _cache.Set("CategoriesCache", cachedCategories, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(1)

                });
            }

            return await _context.SaveChangesAsync();

        }


    }
}



