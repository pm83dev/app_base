using backend.EventManager.Entity;
using backend.EventManager.Events;
using backend.EventManager.EventStore;

namespace backend.EventManager.Commands
{
    public class CommandHandler<TEntity> : ICommands<TEntity> where TEntity : IEntity
    {
        private readonly IEventStore<TEntity> _eventStore;

        public CommandHandler(IEventStore<TEntity> eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task HandleCreateAsync(TEntity entity)
        {
            // Verifica se l'ID è vuoto (guid zero), in tal caso generane uno nuovo
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();  // Genera un nuovo GUID per la creazione
            }

            // Crea un evento di creazione con l'ID appena assegnato
            var createdEvent = new CreatedEvent(entity)
            {
                StreamId = entity.Id  // Imposta il GUID appena generato come StreamId
            };

            var aggregateRoot = new AggregateRoot.AggregateRoot();

            // Applica l'evento all'aggregate root
            aggregateRoot.ApplyEvent(createdEvent);

            // Salva gli eventi non ancora commessi nel event store
            foreach (var @event in aggregateRoot.UncommittedEvents)
            {
                await _eventStore.SaveEventStoreAsync(@event);
            }

            // Pulisci gli eventi non commessi
            aggregateRoot.ClearUncommittedEvents();
        }

        public async Task HandleUpdateAsync(TEntity entity)
        {
            // Usa il GUID esistente dell'entità come StreamId
            var updatedEvent = new UpdatedEvent(entity.Id, entity);  // Passa l'ID dell'entità per lo StreamId

            var aggregateRoot = new AggregateRoot.AggregateRoot();

            // Applica l'evento all'aggregate root
            aggregateRoot.ApplyEvent(updatedEvent);

            // Salva gli eventi non ancora commessi nel event store
            foreach (var @event in aggregateRoot.UncommittedEvents)
            {
                await _eventStore.SaveEventStoreAsync(@event);
            }

            // Pulisci gli eventi non commessi
            aggregateRoot.ClearUncommittedEvents();
        }

        public async Task HandleDeleteAsync(TEntity entity)
        {
            // Crea un evento di cancellazione con l'ID dell'entità e i dati di cancellazione
            var deletedEvent = new DeletedEvent(entity.Id, entity);  // Passa l'ID dell'entità per lo StreamId

            var aggregateRoot = new AggregateRoot.AggregateRoot();

            // Applica l'evento all'aggregate root
            aggregateRoot.ApplyEvent(deletedEvent);

            // Salva gli eventi non ancora commessi nel event store
            foreach (var @event in aggregateRoot.UncommittedEvents)
            {
                await _eventStore.SaveEventStoreAsync(@event);
            }

            // Pulisci gli eventi non commessi
            aggregateRoot.ClearUncommittedEvents();
        }

    }
}
