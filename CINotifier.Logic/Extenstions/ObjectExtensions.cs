namespace CINotifier.Logic.Extenstions
{
    internal static class ObjectExtensions
    {
        internal static bool IsNull<T>(this T @object) where T : class
        {
            return @object == null;
        }

        internal static bool IsNotNull<T>(this T @object) where T : class
        {
            return !IsNull(@object);
        }
    }
}
