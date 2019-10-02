using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace Services.Repositories.Enums
{
    public class EnumExtension
    {
        public class LocalizedDescriptionAttribute : DescriptionAttribute
        {
            private readonly string _resourceKey;
            private readonly ResourceManager _resource;
            public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
            {
                _resource = new ResourceManager(resourceType);
                _resourceKey = resourceKey;
            }

            public override string Description
            {
                get
                {
                    string displayName = _resource.GetString(_resourceKey);

                    return string.IsNullOrEmpty(displayName)
                        ? string.Format("[[{0}]]", _resourceKey)
                        : displayName;
                }
            }
        }

        public static class EnumExtensions
        {
            public static string GetDescription( Enum enumValue)
            {
                FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

                if (attributes != null &&
                    attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return enumValue.ToString();
            }
        }
        public class EnumTools
        {
            public static string GetDescription(Enum en)
            {
                Type type = en.GetType();
                MemberInfo[] memInfo = type.GetMember(en.ToString());
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = null;// memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                        return ((DescriptionAttribute)attrs[0]).Description;
                }
                return en.ToString();
            }
        }
    }
}