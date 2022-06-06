using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Toolkit;
public static class MauiAppBuilderExtensions
{
    public static T? GetService<T>(this MauiAppBuilder builder) where T : class
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        if (builder.Services is null)
            return default(T);

        return builder.Services.GetService<T>();
    }

    public static T? GetService<T>(this IServiceCollection services) where T : class
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        foreach (var service in services)
        {
            if (service is T TService)
                return TService;

            if (typeof(T).IsAssignableFrom(service.GetType()))
                return service as T;
        }

        return default(T);
    }
}
