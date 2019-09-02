using System;
using Microsoft.Extensions.Configuration;

namespace Samwise.Abstractions.Extensions
{
    // TODO Colocar esse método extensivo para IConfiguration no MorganFreemam
    public static class ConfigurationExtensions
    {
        public static string TryGet(this IConfiguration configuration, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            try
            {
                return configuration.GetSection(key).Value;
            }
            catch
            {
                return default;
            }
        }

        public static T TryGet<T>(this IConfiguration configuration, string key)
        {
            return (T)Convert.ChangeType(configuration.TryGet(key), typeof(T));
        }
    }
}