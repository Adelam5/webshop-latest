namespace Application.Common.Interfaces;
public interface ICacheService
{
    Task<T?> Get<T>(string key, CancellationToken cancellationToken = default)
        where T : class;

    Task<T?> Get<T>(string key, Func<Task<T?>> factory, CancellationToken cancellationToken = default)
        where T : class;

    Task Set<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class;

    Task Remove(string key, CancellationToken cancellationToken = default);

    Task RemoveByPrefix(string prefixKey, CancellationToken cancellationToken = default);

}
