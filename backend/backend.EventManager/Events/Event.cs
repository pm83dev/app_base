using backend.EventManager.Entity;

namespace backend.EventManager.Events
{
    public abstract class Event
    {
        public Guid EventId { get; init; }  // ID univoco per l'evento
        public Guid StreamId { get; init; }  // ID dell'oggetto a cui è legato l'evento
        public string EventType { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow; // Data di creazione dell'evento
        public object Data { get; init; } // I dati associati all'evento (può essere un DPI o altre informazioni)

        // Costruttore
        protected Event(Guid streamId, string eventType, object data)
        {
            StreamId = streamId; // Assegna l'ID dell'oggetto a cui è legato l'evento
            EventType = eventType;
            Data = data;
            EventId = Guid.NewGuid(); // Ogni evento ha un ID unico generato automaticamente
        }
    }
}
