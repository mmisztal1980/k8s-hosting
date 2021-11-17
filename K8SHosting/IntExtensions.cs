using System;

namespace K8SHosting
{
    public static class IntExtensions
    {
        public static TimeSpan Seconds(this int value)
        {
            return TimeSpan.FromSeconds(value);
        }

        public static TimeSpan Milliseconds(this int value)
        {
            return TimeSpan.FromMilliseconds(value);
        }
    }
}
