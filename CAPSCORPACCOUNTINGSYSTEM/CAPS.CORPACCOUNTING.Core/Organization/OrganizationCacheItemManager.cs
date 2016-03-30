using Abp.Runtime.Caching;


namespace CAPS.CORPACCOUNTING.Organization
{
    public static class OrganizationCacheItemManager
    {
        public static ITypedCache<string, OrganizationCacheItem> GetOrganizationCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, OrganizationCacheItem>(OrganizationCacheItem.CacheName);
        }
    }
}
