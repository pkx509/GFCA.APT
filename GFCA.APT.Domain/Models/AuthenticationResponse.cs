using GFCA.APT.Domain;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.HTTP.Controls;

namespace GFCA.APT.Domain.Models
{
    public class AuthenticationResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public UserInfoDto User { get; set; } = null;

        private AuthenticationResponse()
        {
        }

        public static AuthenticationResponse SUCCESS(UserInfoDto userInfo)
        {
            return new AuthenticationResponse()
            {
                IsSuccess = true,
                User = userInfo
            };
        }

        public static AuthenticationResponse FAILED(string message)
        {
            return new AuthenticationResponse()
            {
                IsSuccess = false,
                ErrorMessage = message
            };
        }
    }
}
