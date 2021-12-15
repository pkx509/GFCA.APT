using GFCA.APT.Domain.Common;
using GFCA.APT.Domain.HTTP.Controls;

namespace GFCA.APT.Domain.Models
{
    public class BusinessResponse
    {
        public bool Success { get; set; } = false;
        public MESSAGE_TYPE MessageType { get; set; } = MESSAGE_TYPE.WARNING;
        public string Title { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public BusinessResponse()
        {
            Success = false;
            MessageType = MESSAGE_TYPE.WARNING;
            Title = MessageType.ToString();
            Message = string.Empty;
        }
        public BusinessResponse(bool isSuccess, MESSAGE_TYPE messageType, string message)
        {
            Success = isSuccess;
            MessageType = messageType;
            Title = messageType.ToString();
            Message = message;
        }
    }

    public class WorkflowResponse
    {
        public bool Success { get; set; } = false;
        public MESSAGE_TYPE MessageType { get; set; } = MESSAGE_TYPE.WARNING;
        public string Message { get; set; } = string.Empty;
        public dynamic Data { get; set; }

        public WorkflowResponse()
        {

        }
        public WorkflowResponse(bool isSuccess, MESSAGE_TYPE messageType, string message)
        {
            Success = isSuccess;
            MessageType = messageType;
            Message = message;
        }
    }
}
