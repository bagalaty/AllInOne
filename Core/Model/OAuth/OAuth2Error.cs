using System.Runtime.Serialization;

namespace Core.Model.OAuth
{
    [DataContract]
    public class OAuth2Error
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "error_description")]
        public string ErrorDescription { get; set; }

        [DataMember(Name = "code")]
        public int Code { get; set; }

        public bool HasError => !(string.IsNullOrWhiteSpace(Error) && string.IsNullOrWhiteSpace(ErrorDescription));
    }
}