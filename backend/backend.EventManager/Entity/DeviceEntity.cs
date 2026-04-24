using backend.EventManager.Entity;

namespace backend.EventManager.DTOs;

public class DeviceEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
}