
using backend.EventManager.Events;

namespace backend.EventManager.AggregateRoot
{
    public class AggregateRoot
    {
        public Guid streamId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<Event> _uncommittedEvents = new();
        public IEnumerable<Event> UncommittedEvents => _uncommittedEvents;

        public AggregateRoot() { }

        public void ApplyEvent(Event @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            // Applicazione dell'evento all'aggregato
            switch (@event)
            {
                case CreatedEvent createdEvent:
                    Apply(createdEvent);
                    break;
                case UpdatedEvent updatedEvent:
                    Apply(updatedEvent);
                    break;
                case DeletedEvent deletedEvent:
                    Apply(deletedEvent);
                    break;
                default:
                    throw new ArgumentException($"Invalid event type: {@event.GetType().Name}");
            }

            // Aggiungi l'evento agli eventi non commessi
            _uncommittedEvents.Add(@event);
        }

        private void Apply(CreatedEvent createdEvent)
        {
            streamId = createdEvent.StreamId;
            CreatedAt = createdEvent.CreatedAt;
        }

        private void Apply(UpdatedEvent updatedEvent)
        {
            streamId = updatedEvent.StreamId;
            CreatedAt = updatedEvent.CreatedAt;
        }

        private void Apply(DeletedEvent deletedEvent)
        {
            streamId = deletedEvent.StreamId;
            CreatedAt = deletedEvent.CreatedAt;
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }
    }
}

