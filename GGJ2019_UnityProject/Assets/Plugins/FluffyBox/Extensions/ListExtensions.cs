using System.Collections.Generic;

namespace FluffyBox.Extensions
{
    public static class ListExtensions
    {
        public static void AddOnce<T>(this List<T> list, T itemToAdd)
        {
            if (!list.Contains(itemToAdd))
            {
                list.Add(itemToAdd);
            }        
        }
    }
}
