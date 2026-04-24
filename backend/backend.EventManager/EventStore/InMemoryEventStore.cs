using backend.EventManager.Entity;
using backend.EventManager.Events;

namespace backend.EventManager.EventStore
{
    public class InMemoryEventStore<TEntity> : IEventStore<TEntity> where TEntity : class, IEntity
    {
        private readonly Dictionary<Guid, List<Event>> _eventStore = new Dictionary<Guid, List<Event>>();

        public Task SaveEventStoreAsync(Event @event)
        {
            if (!_eventStore.ContainsKey(@event.StreamId))
            {
                _eventStore.Add(@event.StreamId, new List<Event>());
                Console.WriteLine($"{@event.EventType} per ID: {@event.StreamId} dato non trovato, creato nuovo dato");
            }

            _eventStore[@event.StreamId].Add(@event);
            Console.WriteLine($"{@event.EventType} per ID: {@event.StreamId} salvato");

            // Chiamata alla funzione
            PrintObjectProperties(@event.Data);

            

            return Task.CompletedTask;
        }

        public Task<IEnumerable<TEntity>> GetAllItem()
        {
            var entities = _eventStore.Values
                .SelectMany(events => events)  // Unisci tutti gli eventi
                .GroupBy(e => e.StreamId)      // Raggruppa gli eventi per StreamId
                .Select(group => group
                    .OrderByDescending(e => e.CreatedAt)  // Ordina per data decrescente

                    .FirstOrDefault())  // Prendi l'ultimo evento
                .Select(@event => @event?.Data as TEntity) // Estrai l'entità dai dati dell'evento
                .Where(entity => entity != null)  // Filtra i null
                .Select(entity => entity!)  // Assert non-nullability
                .ToList();

            return Task.FromResult<IEnumerable<TEntity>>(entities);
        }




        public Task<IEnumerable<Event>> GetEventsByIdAsync(Guid streamId)
        {
            if (!_eventStore.ContainsKey(streamId))
            {
                return Task.FromResult(Enumerable.Empty<Event>());
            }

            return Task.FromResult(_eventStore[streamId].AsEnumerable());
        }

        //Reflection per estrazione oggetto e tipi nidificati
        void PrintObjectProperties(object obj, int indent = 0)
        {
            if (obj == null) return;

            string indentStr = new string(' ', indent * 4);
            var type = obj.GetType();

            foreach (var prop in type.GetProperties())
            {
                var value = prop.GetValue(obj);

                if (value == null)
                {
                    Console.WriteLine($"{indentStr}{prop.Name}: null");
                }
                else if (prop.PropertyType.Namespace == "System") // Tipo primitivo
                {
                    Console.WriteLine($"{indentStr}{prop.Name}: {value}");
                }
                else // Oggetto complesso → Ricorsione
                {
                    Console.WriteLine($"{indentStr}{prop.Name}:");
                    PrintObjectProperties(value, indent + 1);
                }
            }
        }
    }
}