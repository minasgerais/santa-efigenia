using Sauron.Abstractions.IO;
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
    }
}
