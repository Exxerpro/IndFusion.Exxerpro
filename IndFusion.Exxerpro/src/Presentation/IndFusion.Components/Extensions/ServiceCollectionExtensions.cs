using IndFusion.Components.Interfaces;
using IndFusion.Components.Service;
using IndFusion.Components.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;

namespace IndFusion.Components.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorStrap(this IServiceCollection serviceCollection, Action<BlazorStrapOptions>? options = null)
        {
            serviceCollection.AddScoped<BlazorStrapInterop>();
            serviceCollection.AddScoped<IBlazorStrap>(x => new BlazorStrapCore(x.GetRequiredService<IJSRuntime>(), options));
            serviceCollection.AddScoped<ISvgLoader, SvgLoader>();
            return serviceCollection;
        }
    }
}
