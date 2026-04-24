using System.Collections.Specialized;

namespace backend.EventManager.Events
{
   public class UpdatedEvent : Event
    {
        public UpdatedEvent(Guid dataId, object updatedData)
            : base(dataId, "Updated", updatedData) // Passa lo StreamId al costruttore base
        {
        }
    }
}