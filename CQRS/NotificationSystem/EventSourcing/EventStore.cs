using Marten.Events;
using Marten.Linq;

namespace CQRS.NotificationSystem.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public async Task SaveEventAsync(IEvent @event)
        {
            _events.Add(@event);
            await Task.CompletedTask;
        }

        //public async Task<List<IEvent>> GetEventsAsync(Guid aggregateId)
        //{
        //    return await Task.FromResult(_events.Where(e => e.AggregateId == aggregateId).ToList());
        //}

        public StreamAction Append(Guid stream, long expectedVersion, IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream<TAggregate>(Guid id, IEnumerable<object> events) where TAggregate : class
        {
            throw new NotImplementedException();
        }

        public Task AppendOptimistic(string streamKey, CancellationToken token, params object[] events)
        {
            throw new NotImplementedException();
        }

        public Task AppendOptimistic(string streamKey, params object[] events)
        {
            throw new NotImplementedException();
        }

        public Task AppendOptimistic(Guid streamId, CancellationToken token, params object[] events)
        {
            throw new NotImplementedException();
        }

        public Task AppendOptimistic(Guid streamId, params object[] events)
        {
            throw new NotImplementedException();
        }

        public Task AppendExclusive(string streamKey, CancellationToken token, params object[] events)
        {
            throw new NotImplementedException();
        }

        public Task AppendExclusive(string streamKey, params object[] events)
        {
            throw new NotImplementedException();
        }

        public Task AppendExclusive(Guid streamId, CancellationToken token, params object[] events)
        {
            throw new NotImplementedException();
        }

        public Task AppendExclusive(Guid streamId, params object[] events)
        {
            throw new NotImplementedException();
        }

        public void ArchiveStream(Guid streamId)
        {
            throw new NotImplementedException();
        }

        public void ArchiveStream(string streamKey)
        {
            throw new NotImplementedException();
        }

        public Task<IEventStream<T>> FetchForWriting<T>(Guid id, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteToAggregate<T>(Guid id, Action<IEventStream<T>> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteToAggregate<T>(Guid id, Func<IEventStream<T>, Task> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteToAggregate<T>(string id, Action<IEventStream<T>> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteToAggregate<T>(string id, Func<IEventStream<T>, Task> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEventStream<T>> FetchForWriting<T>(string key, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEventStream<T>> FetchForWriting<T>(Guid id, long expectedVersion, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEventStream<T>> FetchForWriting<T>(string key, long expectedVersion, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteToAggregate<T>(Guid id, int expectedVersion, Action<IEventStream<T>> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteToAggregate<T>(Guid id, int expectedVersion, Func<IEventStream<T>, Task> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteToAggregate<T>(string id, int expectedVersion, Action<IEventStream<T>> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteToAggregate<T>(string id, int expectedVersion, Func<IEventStream<T>, Task> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEventStream<T>> FetchForExclusiveWriting<T>(Guid id, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEventStream<T>> FetchForExclusiveWriting<T>(string key, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteExclusivelyToAggregate<T>(Guid id, Action<IEventStream<T>> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteExclusivelyToAggregate<T>(string id, Action<IEventStream<T>> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteExclusivelyToAggregate<T>(Guid id, Func<IEventStream<T>, Task> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task WriteExclusivelyToAggregate<T>(string id, Func<IEventStream<T>, Task> writing, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public void OverwriteEvent(IEvent e)
        {
            throw new NotImplementedException();
        }

        public ValueTask<T> FetchLatest<T>(Guid id, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public ValueTask<T> FetchLatest<T>(string id, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public StreamAction Append(Guid stream, IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction Append(Guid stream, params object[] events)
        {
            throw new NotImplementedException();
        }

        public StreamAction Append(string stream, IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction Append(string stream, params object[] events)
        {
            throw new NotImplementedException();
        }

        public StreamAction Append(Guid stream, long expectedVersion, params object[] events)
        {
            throw new NotImplementedException();
        }

        public StreamAction Append(string stream, long expectedVersion, IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction Append(string stream, long expectedVersion, params object[] events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream<TAggregate>(Guid id, params object[] events) where TAggregate : class
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(Type aggregateType, Guid id, IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(Type aggregateType, Guid id, params object[] events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream<TAggregate>(string streamKey, IEnumerable<object> events) where TAggregate : class
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream<TAggregate>(string streamKey, params object[] events) where TAggregate : class
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(Type aggregateType, string streamKey, IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(Type aggregateType, string streamKey, params object[] events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(Guid id, IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(Guid id, params object[] events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(string streamKey, IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(string streamKey, params object[] events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream<TAggregate>(IEnumerable<object> events) where TAggregate : class
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream<TAggregate>(params object[] events) where TAggregate : class
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(Type aggregateType, IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(Type aggregateType, params object[] events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(IEnumerable<object> events)
        {
            throw new NotImplementedException();
        }

        public StreamAction StartStream(params object[] events)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IEvent> FetchStream(Guid streamId, long version = 0, DateTimeOffset? timestamp = null, long fromVersion = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<IEvent>> FetchStreamAsync(Guid streamId, long version = 0, DateTimeOffset? timestamp = null, long fromVersion = 0, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IEvent> FetchStream(string streamKey, long version = 0, DateTimeOffset? timestamp = null, long fromVersion = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<IEvent>> FetchStreamAsync(string streamKey, long version = 0, DateTimeOffset? timestamp = null, long fromVersion = 0, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public T? AggregateStream<T>(Guid streamId, long version = 0, DateTimeOffset? timestamp = null, T? state = null, long fromVersion = 0) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T?> AggregateStreamAsync<T>(Guid streamId, long version = 0, DateTimeOffset? timestamp = null, T? state = null, long fromVersion = 0, CancellationToken token = default) where T : class
        {
            throw new NotImplementedException();
        }

        public T? AggregateStream<T>(string streamKey, long version = 0, DateTimeOffset? timestamp = null, T? state = null, long fromVersion = 0) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T?> AggregateStreamAsync<T>(string streamKey, long version = 0, DateTimeOffset? timestamp = null, T? state = null, long fromVersion = 0, CancellationToken token = default) where T : class
        {
            throw new NotImplementedException();
        }

        public IMartenQueryable<T> QueryRawEventDataOnly<T>()
        {
            throw new NotImplementedException();
        }

        public IMartenQueryable<IEvent> QueryAllRawEvents()
        {
            throw new NotImplementedException();
        }

        public IEvent<T>? Load<T>(Guid id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEvent<T>?> LoadAsync<T>(Guid id, CancellationToken token = default) where T : class
        {
            throw new NotImplementedException();
        }

        public IEvent? Load(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEvent?> LoadAsync(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public StreamState? FetchStreamState(Guid streamId)
        {
            throw new NotImplementedException();
        }

        public Task<StreamState?> FetchStreamStateAsync(Guid streamId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public StreamState? FetchStreamState(string streamKey)
        {
            throw new NotImplementedException();
        }

        public Task<StreamState?> FetchStreamStateAsync(string streamKey, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
