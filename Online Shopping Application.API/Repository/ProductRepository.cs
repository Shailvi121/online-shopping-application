
namespace Online_Shopping_Application.API.Repository
{
    public class ProductRepository : Repository<Product>, IProduct
    {
        private readonly FammsContext _context;
        private readonly IMemoryCache _memoryCache;
        public ProductRepository(FammsContext context, IMemoryCache cache) : base(context)
        {
            _context = context;
            _memoryCache = cache;
        }
        public override async Task<IQueryable<Product>> GetAll()
        {
            if (_memoryCache.TryGetValue("CachedAllProducts", out IQueryable<Product> CachedData))
            {
                return CachedData;
            }
            var entity = _context.Set<Product>().AsQueryable();
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(10),

            };
            _memoryCache.Set("CachedAllProducts", entity, cacheEntryOptions);

            return await Task.FromResult(entity);
        }
        public override async Task<Product?> GetById(int id)
        {
            if (_memoryCache.TryGetValue("CachedAllProducts", out List<Product> CachedData))
            {
                var cachedEntity = CachedData.FirstOrDefault(entity => entity.Id == id);

                if (cachedEntity != null)
                {
                    return cachedEntity;
                }
            }

            var entity = await _context.Products.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            if (entity != null)
            {
                CachedData ??= new List<Product>();
                CachedData.Add(entity);

                _memoryCache.Set("CachedAllProducts", CachedData, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(10),
                });
            }

            return entity;
        }

    }


}
