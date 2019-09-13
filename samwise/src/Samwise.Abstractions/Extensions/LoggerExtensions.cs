using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Samwise.Abstractions.Extensions
{
    public static class LoggerExtensions
    {
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public static void Stamp(this ILogger logger, string message)
        {
            logger.LogInformation($"\n[{DateTime.Now.ToString(DateFormat)}]\t{message}\n");
        }

        public static void Stamp<T>(this ILogger logger, string message, T obj = default)
        {
            logger.LogInformation($"\n[{DateTime.Now.ToString(DateFormat)}]\t{message}\n\r\n{JsonConvert.SerializeObject(obj, Formatting.Indented)}\n");
        }
    }
}