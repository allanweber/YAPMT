using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace YAPMT.Framework.Test
{
    public class PrivateSetterCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);

            if (!jsonProperty.Writable)
            {
                var propertyInfo = member as PropertyInfo;

                jsonProperty.Writable = propertyInfo.HasSetAccessor(true);
            }

            return jsonProperty;
        }
    }
}
