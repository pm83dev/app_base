using backend.EventManager.Commands;
using backend.EventManager.DTOs;
using backend.EventManager.EventStore;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ICommands<DeviceEntity> _commandHandler;
        private readonly IEventStore<DeviceEntity> _eventStore;

        public EventController(ICommands<DeviceEntity> commandHandler, IEventStore<DeviceEntity> eventStore)
        {
            _commandHandler = commandHandler;
            _eventStore = eventStore;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DeviceEntity entity)
        {
            // Verifica se l'ID è vuoto (guid zero), in tal caso generane uno nuovo
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();  // Genera un nuovo GUID per la creazione
            } else if (entity.Id == null)
            {
                entity.Id = Guid.NewGuid();  // Genera un nuovo GUID se l'ID è null
            }
            await _commandHandler.HandleCreateAsync(entity);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] DeviceEntity entity)
        {
            await _commandHandler.HandleUpdateAsync(entity);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] Guid entityId)
        {
            var entity = new DeviceEntity { Id = entityId };
            await _commandHandler.HandleDeleteAsync(entity);
            return Ok();
        }

        [HttpGet("all-events")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventStore.GetAllItem();
            return Ok(events);
        }
    }
}