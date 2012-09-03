using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PresentationGenerator.Core.Utility
{
    public static class StringExtensions
    {
        public static string IsFromAutonomy(this string name, bool fromAutonomyquery)
        {
            return fromAutonomyquery ? name.ToUpper(CultureInfo.CurrentCulture) : name;
        }

        public static bool IsNotNullOrEmpty(this string input)
        {
            return !string.IsNullOrEmpty(input);
        }
        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static bool ContainsCaseInsensitive(this string source, string value)
        {
            int results = source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);
            return results == -1 ? false : true;
        }
    }

    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if (list == null)
                return true;
            if (list.Count == 0)
                return true;
            return false;


        }
    }
}
