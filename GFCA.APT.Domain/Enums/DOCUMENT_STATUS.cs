using System.Runtime.Serialization;

namespace GFCA.APT.Domain.Enums
{
    [DataContract(Name = "DocumentStatus")]
    public enum DOCUMENT_STATUS
    {
        [EnumMember(Value = "NONE")] NONE = 0,
        [EnumMember(Value = "DRAFT")] DRAFT = 1,
        [EnumMember(Value = "APPROVAL")] APPROVAL = 2,
        [EnumMember(Value = "REVIEW")] REVIEW = 3,
        [EnumMember(Value = "COMPLETED")] COMPLETED = 4,
        [EnumMember(Value = "REJECTED")] REJECTED = 5,
        [EnumMember(Value = "CANCELLED")] CANCELLED = -1,
    }
    /*
    public struct DOCUMENT_STATUS
    {
        public const string Draft = @"C";
        public const string WaitForApproval = @"W";
        public const string Approved = @"A";
        public const string Return = @"R";
        public const string Delete = @"D";
    }
    */
}
