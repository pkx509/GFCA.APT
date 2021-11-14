using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Enums
{
    [DataContract(Name = "ConditionType")]
    public enum CONDITION_TYPE
    {
        [EnumMember(Value = "NONE")] NONE = 0,
        [EnumMember(Value = "PLANNING")] PLANNING = 1,
        [EnumMember(Value = "ACTUAL")] ACTUAL = 2,
    }

}
