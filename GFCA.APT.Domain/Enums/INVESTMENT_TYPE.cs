using System.Runtime.Serialization;

namespace GFCA.APT.Domain.Enums
{
    [DataContract(Name = "InvestmentType")]
    public enum INVESTMENT_TYPE
    {
        [EnumMember(Value = "BAHT_COMPENSATE")] BAHT_COMPENSATE = 0,
        [EnumMember(Value = "PROMOTION_DISCOUNT")] PROMOTION_DISCOUNT = 1
    }
}
