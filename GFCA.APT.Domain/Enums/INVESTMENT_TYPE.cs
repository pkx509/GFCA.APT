using System.Runtime.Serialization;

namespace GFCA.APT.Domain.Enums
{
    [DataContract(Name = "InvestmentType")]
    public enum INVESTMENT_TYPE
    {
        [EnumMember(Value = "VALUE")] VALUE = 0,
        [EnumMember(Value = "VALUE_PER_UNIT")] VALUE_PER_UNIT = 1,
        [EnumMember(Value = "PERCENT")] PERCENT = 2,
    }
}
