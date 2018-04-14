namespace System.Reflection
{
    public static class PropertyInfoExtensions
    {
        public static bool HasSetAccessor(this PropertyInfo propertyInfo, bool nonPublic)
        {
            return propertyInfo?.GetSetMethod(nonPublic) != null;
        }
    }
}
