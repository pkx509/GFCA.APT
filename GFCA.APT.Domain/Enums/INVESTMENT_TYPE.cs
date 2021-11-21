using System.Runtime.Serialization;

namespace GFCA.APT.Domain.Enums
{
    [DataContract(Name = "InvestmentType")]
    public enum INVESTMENT_TYPE
    {
        [EnumMember(Value = "VALUE")] VALUE = 0,
        [EnumMember(Value = "PERCENT")] PERCENT = 1,
    }
}
