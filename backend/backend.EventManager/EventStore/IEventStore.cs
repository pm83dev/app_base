

using backend.EventManager.Entity;
using backend.EventManager.Events;

namespace backend.EventManager.EventStore;
public interface IEventStore<TEntity> where TEntity : IEntity
{
    Task SaveEventStoreAsync(Event @event);
    Task<IEnumerable<TEntity>> GetAllItem();
    Task<IEnumerable<Event>> GetEventsByIdAsync(Guid streamId);
}

