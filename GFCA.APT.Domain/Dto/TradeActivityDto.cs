using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class TradeActivityDto : Auditable
    {
        [Required]
        public string ACC_CODE { get; set; }
        public string ACTIVITY_CODE { get; set; }
        public string ACTIVITY_TYPE { get; set; }
        public string ACTIVTITY_NAME { get; set; }

        public bool HAS_FIXED_CONTRACT { get; set; } = false;
        public bool CAN_DEDUCTABLE { get; set; }     = false;
        public bool IN_THB_CS { get; set; }          = false;
        public bool IN_GROSS_SALE { get; set; }      = false;
        public bool IN_NOT_SALE { get; set; }        = false;
        public bool OUT_THB_CS { get; set; }         = false;
        public bool OUT_GROSS_SALE { get; set; }     = false;
        public bool OUT_NOT_SALE { get; set; }       = false;
        public bool NO_RELATE_ABS_AMT { get; set; }  = false;

        public string VALUABLE { get; set; }
        public string ACTIVITY_DESC { get; set; }
        public string FLAG_ROW { get; set; }

        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE { get; set; } = false;

    }
}
