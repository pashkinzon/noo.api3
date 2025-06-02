namespace Noo.Api.Core.DataAbstraction.Cache;

public interface ICacheRepository
{
    public Task<T?> GetAsync<T>(string key);
    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    public Task RemoveAsync(string key);
    public Task<int> CountAsync(string pattern = "*");
}
