using System;
using System.Linq;
using System.Collections.Generic;


namespace SigmaSeasonalizerPlugin
{
    internal static class Extensions
    {
        internal static KeyValuePair<double, TValue> At<TValue>(this SortedDictionary<double, TValue> source, int index)
        {
            int n = source.Count;

            if (index == -1)
            {
                KeyValuePair<double, TValue> pair = source.ElementAt(n - 1);

                return new KeyValuePair<double, TValue>(pair.Key - Math.PI * 2, pair.Value);
            }

            if (index == n)
            {
                KeyValuePair<double, TValue> pair = source.ElementAt(0);

                return new KeyValuePair<double, TValue>(pair.Key + Math.PI * 2, pair.Value);
            }

            return source.ElementAt(index);
        }

        internal static T Evaluate<T>(this SortedDictionary<double, T> source, double key)
        {
            int? n = source?.Count;

            if (n > 1)
            {
                KeyValuePair<double, T> b;
                KeyValuePair<double, T> a;

                for (int i = 0; i <= n; i++)
                {
                    b = source.At(i);

                    if (b.Key > key)
                    {
                        a = source.At(i - 1);
                        return (T)Utility.Lerp(a.Value, b.Value, (key - a.Key) / (b.Key - a.Key));
                    }
                }
            }

            return source.ElementAt(0).Value;
        }
    }
}
