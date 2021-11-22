using System.Runtime.Serialization;

namespace GFCA.APT.Domain.Enums
{
    public struct FLAG_ROW
    {
        public const string DELETE = @"D";      //Inactive
        public const string SHOW   = @"S";      //Active
        public const string MANUAL = @"MANUAL"; //Not existing in SAP
    }

    [DataContract(Name = "RowType")]
    public enum ROW_TYPE
    {
        [EnumMember(Value = "S")] SHOW = 0,
        [EnumMember(Value = "D")] DELETE = 1,
        [EnumMember(Value = "M")] MANUAL = 2
    }
}
