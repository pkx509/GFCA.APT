using GFCA.APT.Domain.HTTP.Controls;

namespace GFCA.APT.Domain.Models
{
    public class BusinessResponse
    {
        public bool Success { get; set; } = false;
        public string MessageType { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public BusinessResponse()
        {

        }
        public BusinessResponse(bool isSuccess, string messageType, string message)
        {
            Success = isSuccess;
            MessageType = messageType;
            Message = message;
        }
    }
}
