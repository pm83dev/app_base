
namespace backend.EventManager.Events
{
   public class CreatedEvent : Event
{
    public CreatedEvent(object data)
        : base(Guid.NewGuid(), "Created", data) // Assegna un nuovo GUID per la creazione
    {
    }
}
}