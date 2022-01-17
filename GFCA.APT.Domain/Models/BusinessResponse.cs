using GFCA.APT.Domain.Enums;

namespace GFCA.APT.Domain.Models
{
    public class BusinessResponse
    {
        public bool Success { get; set; } = false;
        public MESSAGE_TYPE MessageType { get; set; } = MESSAGE_TYPE.WARNING;
        public string Title { get; set; } = MESSAGE_TITLE.ERROR;
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

        public static BusinessResponse CreateInstance(MESSAGE_TYPE messgeType, string message = "")
        {
            string messageTitle = messgeType == MESSAGE_TYPE.SUCCESS ? MESSAGE_TITLE.SUCCESS : MESSAGE_TITLE.ERROR;
            BusinessResponse response = BusinessResponse.CreateInstance(messgeType, messageTitle, message);
            return response;
        }
        public static BusinessResponse CreateInstance(MESSAGE_TYPE messgeType, string messageTitle, string message = "")
        {
            BusinessResponse response = new BusinessResponse();
            response.Success = (messgeType == MESSAGE_TYPE.SUCCESS);
            response.MessageType = messgeType;
            response.Message = message;
            response.Title = messageTitle;
            return response;
        }
    }
}
