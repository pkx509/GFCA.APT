using System.Runtime.Serialization;

namespace GFCA.APT.Domain.HTTP.Controls
{
    [DataContract(Name = "DocumentStatus")]
    public enum MESSAGE_TYPE
    {
        [EnumMember(Value = "info")] INFORMATION = 0,
        [EnumMember(Value = "success")] SUCCESS = 1,
        [EnumMember(Value = "warning")] WARNING = 2,
        [EnumMember(Value = "error")] ERROR = -1,

    }
    
    public struct TOAST_TYPE
    {

        public const string INFORMATION = "info";
        public const string SUCCESS = "success";
        public const string WARNING = "warning";
        public const string ERROR = "error";
        
    }
    
}