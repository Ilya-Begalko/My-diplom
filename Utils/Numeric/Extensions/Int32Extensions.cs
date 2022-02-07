using System;

namespace Utils.Numeric.Extensions
{
    public static class Int32Extensions
    {
        public static double ToRadians(this int angle)
        {
            return Math.PI / 180 * angle;
        }
    }
}