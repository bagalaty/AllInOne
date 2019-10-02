using System.Runtime.Serialization;

namespace Core.Model.OAuth
{
    [DataContract]
    public class OAuth2Response : OAuth2Error
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        [DataMember(Name = "jti")]
        public string Jti { get; set; }

        [DataMember(Name = "first_login")]
        public bool FirstLogin { get; set; }

        [DataMember(Name = "force_change_password")]
        public bool? ForceChangePassword { get; set; }
    }
}