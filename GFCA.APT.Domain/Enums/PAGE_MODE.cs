using System.Runtime.Serialization;

namespace GFCA.APT.Domain.Enums
{
    [DataContract(Name = "PageMode")]
    public enum PAGE_MODE
    {
        [EnumMember(Value = "CREATING")] CREATING = 0,
        [EnumMember(Value = "EDITING")] EDITING = 1,
        [EnumMember(Value = "DELETING")] DELETING = 2,
    }
}
