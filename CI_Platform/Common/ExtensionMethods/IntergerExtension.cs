namespace Common.ExtensionMethods
{
    public static class IntegerExtension
    {
        public static bool IsNullOrZero(this long? id)
        {
            return id == default;
        }
    }
}
