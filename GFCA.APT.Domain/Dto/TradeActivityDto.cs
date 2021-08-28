using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class TradeActivityDto : Auditable
    {
        [Required]
        public int? ACTIVITY_ID { get; set; }
        [Required]
        public int? ACC_ID { get; set; }
        [Required]
        public string ACTIVITY_CODE { get; set; }
        public string ACTIVITY_TYPE { get; set; }
        public string ACTIVTITY_NAME { get; set; }
        public string HAS_FIXED_CONTRACT { get; set; }
        public string CAN_DEDUCTABLE { get; set; }
        public string IN_THB_CS { get; set; }
        public string IN_GROSS_SALE { get; set; }
        public string IN_NOT_SALE { get; set; }
        public string OUT_THB_CS { get; set; }
        public string OUT_GROSS_SALE { get; set; }
        public string OUT_NOT_SALE { get; set; }
        public string NO_RELATE_ABS_AMT { get; set; }
        public string VALUABLE { get; set; }
        public string ACTIVITY_DESC { get; set; }
        public string FLAG_ROW { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE { get; set; } = false;
    }
}
