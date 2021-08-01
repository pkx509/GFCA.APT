namespace GFCA.APT.Domain.Models
{
    public class BusinessResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
