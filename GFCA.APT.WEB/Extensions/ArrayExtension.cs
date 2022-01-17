using System.Collections.Generic;

namespace GFCA.APT.WEB.Extensions
{
    public static class ArrayExtension
    {
        public static T[] Concat<T>(this T[] first, T[] second)
        {
            if (first == null)
            {
                return second;
            }
            if (second == null)
            {
                return first;
            }

            List<T> list = new List<T>(first);
            list.AddRange(second);
            return list.ToArray();
        }
    }
}