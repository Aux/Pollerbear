using Pollerbear.Abstractions;

namespace Pollerbear.Services;

/// <summary>
///     Queue management handled in memory.
/// </summary>
public class MemoryQueueingService : IQueueingService
{
    public Task<IReadOnlyCollection<string>> TakeAsync() => throw new NotImplementedException();
    public Task<string> AddAsync(string channelId) => throw new NotImplementedException();
    public Task RemoveAsync(string channelId) => throw new NotImplementedException();
}
