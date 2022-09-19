using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace MonkeyFinances.Identidade.Api.Entensions
{
    /// <summary>
    /// Extensões de serialização json
    /// </summary>
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
}
