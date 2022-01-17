using System.Runtime.Serialization;

namespace GFCA.APT.Domain.Enums
{
    [DataContract(Name = "MessageType")]
    public enum MESSAGE_TYPE
    {
        [EnumMember(Value = "info")] INFORMATION = 0,
        [EnumMember(Value = "success")] SUCCESS = 1,
        [EnumMember(Value = "warning")] WARNING = 2,
        [EnumMember(Value = "error")] ERROR = -1,

    }
}
