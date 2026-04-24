using backend.repository.DataManager;
using Microsoft.Extensions.DependencyInjection;

namespace backend.factory;

public static class InjectAppServices
{
    public static IServiceCollection InjectApplicationServices(this IServiceCollection self)
    {
        self.AddScoped<DataDLL>();
        return self;

    }
}
