using backend.EventManager.Commands;
using backend.EventManager.DTOs;
using backend.EventManager.EventStore;
using backend.factory.ServiceManager;
using backend.repository.DataManager;
using Microsoft.Extensions.DependencyInjection;

namespace backend.factory;

public static class InjectAppServices
{
    public static IServiceCollection InjectApplicationServices(this IServiceCollection self)
    {
        self.AddScoped<DataDLL>();
        self.AddScoped<DataBLL>();
        self.AddScoped<ICommands<DeviceEntity>, CommandHandler<DeviceEntity>>();
        self.AddSingleton<IEventStore<DeviceEntity>, InMemoryEventStore<DeviceEntity>>();
        return self;

    }
}
