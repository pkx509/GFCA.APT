using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class GLAccountDto : Auditable
    {
        [Required]
        public int ACC_ID { get; set; }
        public int IO_ID { get; set; }
        public int CENTER_ID { get; set; }
        public int FUND_ID { get; set; }
        public string FUND_CENTER_ID { get; set; }
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

