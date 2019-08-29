using System;

namespace Sauron.Abstractions.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTimeOffset ToDateTimeOffset(this DateTime dt, TimeSpan offset)
        {
            if (dt == DateTime.MinValue)
                return DateTimeOffset.MinValue;

            return new DateTimeOffset(dt.Ticks, offset);
        }

        public static DateTimeOffset ToDateTimeOffset(this DateTime dt, double offsetInHours = 0)
        {
            return ToDateTimeOffset(dt, offsetInHours == 0 ? TimeSpan.Zero : TimeSpan.FromHours(offsetInHours));
        }
    }
}
