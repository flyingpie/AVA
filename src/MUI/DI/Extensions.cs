using MUI.DI;
using System.Collections.Generic;

namespace System.Linq
{
    public static class Extensions
    {
        public static IEnumerable<T> Wireup<T>(this IEnumerable<T> source, IContainer container)
        {
            foreach (var item in source)
            {
                container.Wireup(item);

                yield return item;
            }
        }
    }
}