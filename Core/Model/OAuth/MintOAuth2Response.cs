using System.Runtime.Serialization;

namespace Core.Model.OAuth
{
    [DataContract]
    public class AllInOneOAuth2Response : OAuth2Response
    {
        [DataMember(Name = "type")]
        public UserTypes Type { get; set; }

        [DataMember(Name = "b2c")]
        public bool IsB2C { get; set; }
    }

    public enum UserTypes
    {
        USER,
        SUPER_ADMIN,
        //  [EnumExtension.LocalizedDescriptionAttribute("UserType_Admin", typeof(EnumResources))]
        SYSTEM_ADMIN,
        FOUNDATION_ADMIN,
        ADMIN
    }
}