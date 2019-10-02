using System.Runtime.Serialization;

namespace Services.Models.Enums
{
    [DataContract]
    public enum SpaceRole : byte
    {
        [DataMember(Name = "VIEWER")]
        Viewer,
        [DataMember(Name = "COLLABORATOR")]
        Collaborator,
        [DataMember(Name = "EDITOR")]
        Editor,
        [DataMember(Name = "CO_OWNER")]
        Co_Owner,
        [DataMember(Name = "OWNER")]
        Owner
    }
}