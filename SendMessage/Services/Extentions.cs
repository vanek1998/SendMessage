using System.Runtime.CompilerServices;

namespace SendMessage.Services
{
    public static class Extentions
    {
        public static void Foreach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
