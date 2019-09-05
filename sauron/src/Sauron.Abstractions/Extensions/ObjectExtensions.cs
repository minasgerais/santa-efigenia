using Sauron.Abstractions.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Sauron.Abstractions.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetCrc(this object obj)
        {
            using (var crc = new CrcStream(new MemoryStream(Encoding.UTF8.GetBytes(obj.ToString()))))
            {
                using (var rdr = new StreamReader(crc))
                {
                    rdr.ReadToEnd();
                    return crc.ReadCrc.ToString("X8");
                }
            }
        }

        public static IDictionary<string, string> ToDictionary(this object obj)
        {
            if (obj == null)
                throw new NullReferenceException("Unable to convert anonymous object to a dictionary. The source anonymous object is null.");

            var dictionary = new Dictionary<string, string>();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj))
            {
                var value = property.GetValue(obj);
                dictionary.Add(property.Name, value?.ToString());
            }

            return dictionary;
        }
    }
}
