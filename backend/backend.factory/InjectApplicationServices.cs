using Microsoft.Extensions.DependencyInjection;

namespace backend.factory;

public static class InjectAppServices
{
    public static IServiceCollection InjectApplicationServices(this IServiceCollection self) 
        {
            // self.AddScoped<IAccountService, AccountService>();
            // self.AddScoped<IDeviceDataService, DeviceDataService>();
            // self.AddScoped<DppMachineService>();
            // self.AddAutoMapper(typeof(MappingProfile));
            // self.AddHostedService<MqttBackgroundService>();
            return self;
        
        }
}
