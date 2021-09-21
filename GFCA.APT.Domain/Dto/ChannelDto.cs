using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class ChannelDto : Auditable
    {
        //[Required]
        //public int? CHANNEL_ID { get; set; }
        [Required]
        public string CHANNEL_CODE { get; set; }
        public string CHANNEL_NAME { get; set; }
        public string CHANNEL_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
