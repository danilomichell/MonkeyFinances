using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace MonkeyFinances.Core;

public static class JsonExtensions
{
    /// <summary>
    /// Configurações de serialização json
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IMvcBuilder AddCustomJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return builder;
    }
}