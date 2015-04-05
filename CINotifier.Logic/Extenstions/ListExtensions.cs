using System.Collections.Generic;

namespace CINotifier.Logic.Extenstions
{
    internal static class ListExtensions
    {
        internal static void AddRange<T>(this IList<T> list, IList<T> items) where T : class
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
        }
    }
}
