﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using i18n;

namespace DAL
{
    public static class Extensions
    {
        private static readonly Regex ToUnderScoreRegex = new Regex(@"((?<=.)[A-Z][a-zA-Z]*)|((?<=[a-zA-Z])\d+)", RegexOptions.Compiled);

        private static readonly Regex DigitRegex = new Regex(@"[^0-9]+", RegexOptions.Compiled);

        #region List

        public static List<List<T>> ChunkBy<T>(this List<T> items, int sliceSize)
        {
            List<List<T>> list = new List<List<T>>();
            for (int i = 0; i < items.Count; i += sliceSize)
            {
                list.Add(items.GetRange(i, Math.Min(sliceSize, items.Count - i)));
            }
            return list;
        }

        #endregion

        #region Exception

        public static Exception GetOriginalException(this Exception ex)
        {
            return ex.InnerException == null ? ex : ex.InnerException.GetOriginalException();
        }

        public static int LineNumber(this Exception ex)
        {
            int lineNumber = 0;
            const string lineSearch = ":line ";
            int index = ex.StackTrace.LastIndexOf(lineSearch, StringComparison.OrdinalIgnoreCase);
            if (index == -1)
            {
                return lineNumber;
            }

            string lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
            if (int.TryParse(lineNumberText, out lineNumber))
            {
            }

            return lineNumber;
        }

        #endregion

        #region String
        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string CleanFileName(this string fileName)
        {
            return
                Path.GetInvalidFileNameChars()
                    .Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty))
                    .Replace(" ", string.Empty);
        }

        public static string ToCannonicalString(this string value)
        {
            return value?.ToLower().Trim();
        }

        public static string ToUnderScore(this string value)
        {
            return value != null ? ToUnderScoreRegex.Replace(value.ToLower().Trim(), "_") : null;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || value.Equals("null");
        }

        public static string ExSubString(this string value, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            length = value.Length < length ? value.Length : length;
            return value.Substring(startIndex, length);
        }

        public static string ToLowerUnderScore(this string value)
        {
            return
                Regex.Replace(value.Replace("UUID", "Uuid").Replace("ID", "Id"),
                    "(?<=.)([A-Z])",
                    "_$0",
                    RegexOptions.Compiled).ToLower();
        }

        public static string ToCamelCase(this string value)
        {
            if (value == null ||
                value.Length < 2)
            {
                return value;
            }

            string[] words = value.Split(new char[]
                                         {
                                         },
                StringSplitOptions.RemoveEmptyEntries);

            string result = words.FirstOrDefault()?.ToLower();

            foreach (string word in words)
            {
                result += word.Substring(0, 1).ToUpper();
                result += word.Substring(1);
            }

            return result;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static int? ParseID(this string idView)
        {
            string tmp = DigitRegex.Replace(idView, string.Empty);
            int id;
            int.TryParse(tmp, out id);

            return id;
        }

        #endregion

        #region Enums

        public static string GetDisplayName(this Enum e)
        {
            ResourceManager rm = new ResourceManager(typeof(Resources));
            string resourceDisplayName = rm.GetString(e.GetType().Name + "_" + e);

            return string.IsNullOrWhiteSpace(resourceDisplayName) ? e.ToString() : resourceDisplayName;
        }

        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());

            StringValueAttribute[] attribs =
                fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attribs != null && attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        #endregion

        #region DateTime

        public static DateTime StartOfDay(this DateTime date)
        {
            return date.Date;
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 998);
        }

        public static DateTime Yesterday(this DateTime date)
        {
            return DateTime.Today.AddDays(-1);
        }

        public static DateTime ThisWeekStart(this DateTime date)
        {
            return DateTime.Today.AddDays(-(DateTime.Today.DayOfWeek - DayOfWeek.Monday));
        }

        public static DateTime ThisWeekEnd(this DateTime date)
        {
            return DateTime.Today.AddDays(-(DateTime.Today.DayOfWeek - DayOfWeek.Monday) + 6);
        }

        public static DateTime LastWeekStart(this DateTime date)
        {
            return DateTime.Today.AddDays(-(DateTime.Today.DayOfWeek - DayOfWeek.Monday) - 7);
        }

        public static DateTime LastWeekEnd(this DateTime date)
        {
            return DateTime.Today.AddDays(-(DateTime.Today.DayOfWeek - DayOfWeek.Monday) - 1);
        }

        public static DateTime ThisMonthStart(this DateTime date)
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        }

        public static DateTime ThisMonthEnd(this DateTime date)
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
        }

        #endregion
    }
}