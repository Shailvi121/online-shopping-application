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
        var entity = await _context.Categories.ToListAsync();
        _cacheManager.Set(Constants.CacheKeys.CategoryKey, entity);
        return entity.AsQueryable();
    }

    public override async Task<Category?> GetById(int id)
    {
        var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey);
        var cachedEntity = cachedCategory?.FirstOrDefault(e => e.ID == id);
        if (cachedEntity != null)
        {
            return cachedEntity;
        }

        var entity = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(e => e.ID == id);

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
        var cachedEntity = cachedCategory?.FirstOrDefault(predicate.Compile());

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

            _cacheManager.Set(Constants.CacheKeys.CategoryKey, cachedCategory);
        }

        return entity;
    }

    public override async Task Create(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey) ?? new List<Category>();
        cachedCategory.Add(category);
        _cacheManager.Set(Constants.CacheKeys.CategoryKey, cachedCategory);
    }

    public override async Task Update(int id, Category entity)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category != null)
        {
            _context.Entry(category).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey);
            if (cachedCategory != null)
            {
                var index = cachedCategory.FindIndex(g => g.ID == id);
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
        var category = await GetById(id);
        if (category == null)
        {
            return 0;
        }

        var cachedCategory = _cacheManager.Get(Constants.CacheKeys.CategoryKey);
        if (cachedCategory != null)
        {
            cachedCategory.Remove(category);
            _cacheManager.Set(Constants.CacheKeys.CategoryKey, cachedCategory);
        }

        return await _context.SaveChangesAsync();
    }
}
