namespace AspNetAuth;

static class DateExtensions
{
    const string DateFormat = "yyyy-MM-dd";

    public static string ToDateString(this DateTime date) => date.ToString(DateFormat);

    public static DateTime ToDate(this string dateString) => DateTime.ParseExact(dateString, DateFormat, null);
}
