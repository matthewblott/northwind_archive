using System;
using System.Collections.Generic;
using System.Linq;

namespace northwind.web.ui.filters
{
  public static class DictionaryExtensions
  {
    public static string GetValueFromKey(this IDictionary<string, object> dictionary, string keyName) => 
      dictionary.FirstOrDefault(x => x.Key == keyName).Value?.ToString();

    public static T GetValueFromKey<T>(this IDictionary<string, object> dictionary, string keyName)
    {
      var value = GetValueFromKey(dictionary, keyName);

      if ((typeof(T) == typeof(int) || typeof(T) == typeof(string)) && !string.IsNullOrWhiteSpace(value))
      {
        return (T)Convert.ChangeType(value, typeof(T));
      }

      return default;

    }

  }
  
}