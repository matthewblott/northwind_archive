namespace northwind.common
{
  using System;
  
  public static class DateTimeExtensions
  {
    public static string ToInternationalDateString(this DateTime value) => value.ToString(DateFormats.InternationalDate);
    public static string ToInternationalDateTimeString(this DateTime value) => value.ToString(DateFormats.InternationalDateTime);

    public static bool IsMinValue(this DateTime value) => value == DateTime.MinValue;

  }
  
}