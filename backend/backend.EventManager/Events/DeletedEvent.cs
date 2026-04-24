namespace backend.EventManager.Events
{
   public class DeletedEvent : Event
    {
        public DeletedEvent(Guid dataId, object deleteData): base(dataId, "Deleted", deleteData) // Passa lo StreamId al costruttore base
        {
        }
    }
}