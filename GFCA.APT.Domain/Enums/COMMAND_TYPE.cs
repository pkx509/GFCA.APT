using System.Runtime.Serialization;

namespace GFCA.APT.Domain.Enums
{
    [DataContract(Name = "CommandType")]
    public enum COMMAND_TYPE
    {
        [EnumMember(Value = "NONE")] NONE = 0,
        [EnumMember(Value = "SUBMIT")] SUBMIT = 1, 
        [EnumMember(Value = "CONFIRM")] CONFIRM = 2,
        [EnumMember(Value = "CANCEL")] CANCEL = 3,
        [EnumMember(Value = "APPROVE")] APPROVE = 4,
        [EnumMember(Value = "REJECT")] REJECT = 5,
        [EnumMember(Value = "REVIEW")] REVIEW = 6,
        [EnumMember(Value = "COMMIT")] COMMIT = 7
    }
}
