using GFCA.APT.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class FixedContractDetailDto : Auditable
    {
        [Required]
        public int DOC_FCH_ID { get; set; }
        [Required]
        public int DOC_FCD_ID { get; set; }
        [Required]
        public string DOC_CODE { get; set; }
        public int? DOC_VER { get; set; }
        public int? DOC_REV { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string ACTIVITY_CODE { get; set; }
        public string ACTIVITY_NAME { get; set; }
        public string CENTER_CODE { get; set; }
        public string CENTER_NAME { get; set; }
        public string ACC_CODE { get; set; }
        public string ACC_NAME { get; set; }
        public string SIZE { get; set; }
        public string UOM { get; set; }
        public string PACK { get; set; }
        public string DATE_REF { get; set; }
        public CONDITION_TYPE CONDITION_TYPE { get; set; }
        public string CONTRACT_CATE { get; set; }
        public string CONTRACT_DESC { get; set; }
        public Nullable<decimal> M01 { get; set; }
        public Nullable<decimal> M02 { get; set; }
        public Nullable<decimal> M03 { get; set; }
        public Nullable<decimal> M04 { get; set; }
        public Nullable<decimal> M05 { get; set; }
        public Nullable<decimal> M06 { get; set; }
        public Nullable<decimal> M07 { get; set; }
        public Nullable<decimal> M08 { get; set; }
        public Nullable<decimal> M09 { get; set; }
        public Nullable<decimal> M10 { get; set; }
        public Nullable<decimal> M11 { get; set; }
        public Nullable<decimal> M12 { get; set; }
        public string REMARK { get; set; }
        public DOCUMENT_STATUS DOC_STATUS { get; set; }
        public Nullable<DateTime> START_DATE { get; set; }
        public Nullable<DateTime> END_DATE { get; set; }
        public string FLAG_ROW { get; set; }

    }
}
