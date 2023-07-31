using Common.Constants;
using Microsoft.Extensions.Caching.Memory;
using Services.Interfaces;

namespace Services.Implementations;
public class TokenBlacklistService : ITokenBlacklistService
{
    private const string CacheKey = SystemConstant.CACHE_KEY;
    private readonly IMemoryCache _cache;

    public TokenBlacklistService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void AddTokenToBlacklist(string token, DateTime expiration)
    {
        var blacklist = GetBlacklist();
        blacklist[token] = expiration;
        _cache.Set(CacheKey, blacklist);
    }

    public bool IsTokenBlacklisted(string token)
    {
        var blacklist = GetBlacklist();
        return blacklist.ContainsKey(token);
    }

    private IDictionary<string, DateTime> GetBlacklist()
    {
        if (_cache.TryGetValue(CacheKey, out IDictionary<string, DateTime> blacklist))
        {
            return blacklist;
        }

        blacklist = new Dictionary<string, DateTime>();
        _cache.Set(CacheKey, blacklist, TimeSpan.FromMinutes(60)); // Cache expiration time (e.g., 60 minutes)
        return blacklist;
    }
}