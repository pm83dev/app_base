using backend.EventManager.Commands;
using backend.EventManager.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ICommands<DeviceEntity> _commandHandler;

        public EventController(ICommands<DeviceEntity> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DeviceEntity entity)
        {
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
        public async Task<IActionResult> Delete([FromBody] DeviceEntity entity)
        {
            await _commandHandler.HandleDeleteAsync(entity);
            return Ok();
        }
    }
}