﻿using Microsoft.Extensions.Configuration;
using System;

namespace Sauron.Abstractions.Extensions
{
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
