namespace Online_Shopping_Application.API.CacheManager
{
    public class CacheManager<T> where T : class
    {
        private readonly IMemoryCache _memoryCache;
        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache=memoryCache;
        }
        public List<T> Get(string key) 
        {
            var ListTEntity = default(List<T>);  
            
            if (_memoryCache.TryGetValue(key, out List<T> cachedItems))
            {
                ListTEntity = cachedItems;
            }
            return ListTEntity;
        }
        public List<T> Set(string key, List<T> entities)
        {
            return _memoryCache.Set(key, entities, new MemoryCacheEntryOptions
            { 
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(3)
            });
        }

    }
}
