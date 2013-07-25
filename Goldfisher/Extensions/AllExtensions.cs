using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goldfisher
{
    public static class AllExtensions
    {
        private static readonly Random _random = new Random();


        public static string FormatWith(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static IEnumerable<T> AsRandomized<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                var r = _random.Next(0, list.Count);
                list[i] = list[r];
                list[r] = temp;
            }

            return list;
        }

        public static List<T> Copy<T>(this List<T> list)
        {
            var array = list.ToArray();
            return array.ToList();
        }

        public static bool EqualsAny<T>(this T obj, params T[] objects)
        {
            if (objects == null)
                throw new ArgumentNullException("objects");
            return objects.Contains(obj);
        }

        public static void AddRange<T>(this List<T> list, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            items.ToList().ForEach(list.Add);
        }

        public static string Repeat(this char c, int times)
        {
            if (times < 0)
                throw new ArgumentOutOfRangeException("times");
            var text = new StringBuilder();
            text.Append(c, times);
            return text.ToString();
        }

        public static void Add<T, U, V>(this List<Tuple<T, U, V>> list, T first, U second, V third)
        {
            var tuple = new Tuple<T, U, V>(first, second, third);
            list.Add(tuple);
        }
    }
}
