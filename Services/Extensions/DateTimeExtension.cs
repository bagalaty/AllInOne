using System.Globalization;
using Services.Helpers;

namespace System
{
    public static class DateTimeExtension
    {
        private const string DateTimeFormat = "dd-MM-yyyy hh:mm tt";

        public const string UniversalDateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";

        public static string GetDateString(this DateTime date)
        {
            return date.ToLocalTime().ToString(AppConstants.ClientSideMappedDateFormat, CultureInfo.InvariantCulture);
        }

        public static string GetDateTimeString(this DateTime date, string format = DateTimeFormat)
        {
            return date.ToLocalTime().ToString(format, CultureInfo.InvariantCulture);
        }

        public static string GetUniversalDateTimeString(this DateTime date)
        {
            return date.ToUniversalTime().ToString(UniversalDateTimeFormat, CultureInfo.InvariantCulture);
        }

        public static DateTime GetUniversalDateTime(this string value)
        {
            DateTime daoDateTime;
            DateTime.TryParseExact(value, AppConstants.ClientSideMappedDateFormat, null, DateTimeStyles.AdjustToUniversal, out daoDateTime);
            return daoDateTime.ToUniversalTime();
        }

        public static DateTime? GetDateTime(this string value, string format = UniversalDateTimeFormat)
        {
            DateTime daoDateTime;
            return DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out daoDateTime) ? daoDateTime : default(DateTime?);
        }

        public static string ToEnglishFormat(this DateTime? data, string format)
        {
            if (!data.HasValue)
                return "";
            var response = data.Value.ToString(format, new CultureInfo("es-US"));
            return response;
        }

        public static string ToShortDateString(this DateTime date)
        {
            return date.ToString(AppConstants.ClientSideMappedDateFormat);
        }
    }
}