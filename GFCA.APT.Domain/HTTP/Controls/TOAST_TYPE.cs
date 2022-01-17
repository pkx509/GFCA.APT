using System.Runtime.Serialization;

namespace GFCA.APT.Domain.HTTP.Controls
{    
    public struct TOAST_TYPE
    {
        public const string INFORMATION = "info";
        public const string SUCCESS = "success";
        public const string WARNING = "warning";
        public const string ERROR = "error";
        
    }

    public struct RESPONSE_TITLE
    {
        public const string SUCCESS = "Success";
        public const string ERROR = "Error";
    }
    
}