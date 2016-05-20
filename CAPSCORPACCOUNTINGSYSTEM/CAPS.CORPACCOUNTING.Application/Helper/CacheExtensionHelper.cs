using Abp.Runtime.Caching;

namespace CAPS.CORPACCOUNTING.Helpers
{
    public static class CacheExtensionHelper
    {
        public static ITypedCache<string, CacheItem> GetCacheItem(this ICacheManager cacheManager,string CacheStoreName)
        {
            return cacheManager.GetCache<string, CacheItem>(CacheStoreName);
        }
    }
}
