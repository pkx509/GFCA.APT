using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class GLAccountDto : Auditable
    {
        public string IO_CODE { get; set; }
        public string CENTER_CODE { get; set; }
        public string FUND_ID { get; set; }
        public string FUND_CENTER_ID { get; set; }
        public string GRP_CODE { get; set; }
        [Required]
        public string ACC_CODE { get; set; }
        public string ACC_NAME { get; set; }
        public string ACC_TYPE { get; set; }
        public string ACC_TYPE_DESC { get; set; }
        public string ACC_GROUP1 { get; set; }
        public string ACC_GROUP1_DESC { get; set; }
        public string ACC_GROUP2 { get; set; }
        public string ACC_GROUP2_DESC { get; set; }
        public string ACC_REMARK { get; set; }
        public string FLAG_ROW { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
    }
}

