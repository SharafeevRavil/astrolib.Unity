using System;

namespace Helpers
{
    public static class CoordinateConverter
    {
        public static string ConvertRa(double rightAscension)
        {
            var hours = (int)(rightAscension / 15);
            var minutes = (int)(rightAscension % 15 * 4);
            var seconds = (rightAscension % 15 * 4 - minutes) * 60;

            return $"{hours}h {minutes}m {seconds:F2}s";
        }

        public static string ConvertDec(double declination)
        {
            var degrees = (int)Math.Abs(declination);
            var minutes = (int)((Math.Abs(declination) - degrees) * 60);
            var seconds = ((Math.Abs(declination) - degrees) * 60 - minutes) * 60;

            var sign = declination >= 0 ? "+" : "-";

            return $"{sign}{degrees}° {minutes}' {seconds:F2}\"";
        }
    }
}