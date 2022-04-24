using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amterp.Utils {
    public static class CSharpUtil {
        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0);
        private static System.Random RNG = new System.Random();

        // STANDALONE STATICS

        public static long CurrentTimeMillis() {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public static long CurrentTimeNanos() {
            return (long)((DateTime.UtcNow - EPOCH).TotalMilliseconds * 1000000.0);
        }

        // FORMATTING

        public static string FormatAsPercent(this float thisFloat) {
            return string.Format("{0:P1}", thisFloat);
        }

        // todo clean up with a StringBuilder, if C# has one.
        public static string FormatAsHHMMSS(this TimeSpan timeSpan,
                bool includeHoursIfZero = false,
                bool includeMinutesIfZero = false,
                bool includeLeadingZeroInHours = false,
                bool includeLeadingZeroInMinutes = false,
                bool includeLeadingZeroInSeconds = false) {
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;
            if (hours > 0 || includeHoursIfZero) {
                if (includeLeadingZeroInHours) return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
                return string.Format("{0:D1}:{1:D2}:{2:D2}", hours, minutes, seconds);
            }
            if (minutes > 0 || includeMinutesIfZero) {
                if (includeLeadingZeroInMinutes) return string.Format("{0:D2}:{1:D2}", minutes, seconds);
                return string.Format("{0:D1}:{1:D2}", minutes, seconds);
            }
            if (includeLeadingZeroInSeconds) return string.Format("{0:D2}", seconds);
            return string.Format("{0:D1}", seconds);
        }

        public static string ToDetailedString(this Vector3 vector3) {
            return $"({vector3.x}, {vector3.y}, {vector3.z})";
        }

        // UNITY EXTENSIONS

        public static Vector2 ToVector2DroppingY(this Vector3 vector3) {
            return new Vector2(vector3.x, vector3.z);
        }

        public static Vector3 ZeroY(this Vector3 vector3) {
            return new Vector3(vector3.x, 0, vector3.z);
        }

        public static Vector3 To3d(this Vector2 v) {
            return new Vector3(v.x, 0, v.y);
        }


        // C# EXTENSIONS

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (var item in enumerable)
                action(item);
        }

        public static T Last<T>(this T[] array) {
            return array[array.Length - 1];
        }

        public static T Sample<T>(this IList<T> collection) {
            return collection[RNG.Next(0, collection.Count - 1)];
        }

        public static string Capitalize(this string thisString) {
            return char.ToUpper(thisString[0]) + thisString.Substring(1);
        }

        public static float normalized(this float thisFloat) {
            if (Mathf.Approximately(thisFloat, 0)) return 0f;
            if (thisFloat < 0) return -1;
            return 1;
        }

        public static bool isAboutZero(this float thisFloat) {
            return Mathf.Approximately(0, thisFloat);
        }

        public static void SafeInvoke<T>(this Action<T> action, T arg) {
            if (action != null) action(arg);
        }

        public static void SafeInvoke<T, U>(this Action<T, U> action, T arg1, U arg2) {
            if (action != null) action(arg1, arg2);
        }

        public static V GetValueOrDefault<K, V>(this IDictionary<K, V> dictionary, K key, V defaultValue) {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static V GetValueOrDefault<K, V>(this IDictionary<K, V> dictionary, K key, Func<V> defaultValueProvider) {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValueProvider();
        }
    }
}
