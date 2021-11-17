using GFCA.APT.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class PromotionPlanningDto : Auditable
    {
        public PAGE_MODE DataMode { get; set; }
        public DocumentStateFlowDto Stateflow { get; set; }
        public PromotionPlanningHeaderDto Header { get; set; }
        public PromotionPlanningDetailDto Detail { get; set; }
        public PromotionPlanningFooterDto Footer { get; set; }
        public IEnumerable<DocumentHistoryDto> Histories { get; set; }
    }

    public class PromotionPlanningHeaderDto : Auditable
    {
        [Required]
        /* Document */
        public string DOC_TYPE_CODE { get; set; }
        //[Required]
        public string DOC_CODE { get; set; }
        public int? DOC_VER { get; set; } = 0;
        public int? DOC_REV { get; set; } = 0;
        [Required]
        public int DOC_MONTH { get; set; }
        [Required]
        public int DOC_YEAR { get; set; }
        public DOCUMENT_STATUS DOC_STATUS { get; set; }
        public string FLOW_CURRENT { get; set; }
        public string FLOW_NEXT { get; set; }
        public string REQUESTER { get; set; }

        [Required]
        /* Promotion Planning Header */
        public int DOC_PROPLH_ID { get; set; }
        public string PROGP_CODE { get; set; }
        public string CLIENT_CODE { get; set; }
        public string CLIENT_NAME { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_NAME { get; set; }
        public string CHANNEL_CODE { get; set; }
        public string CHANNEL_NAME { get; set; }
        public string PROPL_CODE { get; set; }
        public string PROPL_NAME { get; set; }
        public string PROPL_BEGIN {get; set; }
        public string PROPL_END { get; set; }
        public string COMMENT { get; set; }
        public string FLAG_ROW { get; set; }
        public COMMAND_TYPE COMMAND_TYPE { get; set; } = COMMAND_TYPE.NONE; //SUBMIT, CANCEL, APPROVE, REVIEW, CONFIRM, COMMIT
    }
    public class PromotionPlanningDetailDto : Auditable
    {
        [Required]
        public int DOC_PROPLH_ID { get; set; }
        [Required]
        public int DOC_PROPLD_ID { get; set; }
        
        [Required]
        public string DOC_CODE { get; set; }
        public string PROPLD_TYPE_CODE { get; set; }
        public int? DOC_VER { get; set; }
        public int? DOC_REV { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string SKU_CODE { get; set; }
        public string SKU_NAME { get; set; }

        /* NORMAL column group */
        public Nullable<decimal> NORMAL_DISCOUNT_PERCENTAGE { get; set; }
        public Nullable<decimal> NORMAL_GP_PERCENTAGE { get; set; }
        public Nullable<decimal> NORMAL_SHELF_PRICE { get; set; }
        /* PROMOTION column group*/
        public Nullable<decimal> PROMOTION_DISCOUNT_PERCENTAGE { get; set; }
        public Nullable<decimal> PROMOTION_GP_PERCENTAGE { get; set; }
        public Nullable<decimal> PROMOTION_SHELF_PRICE { get; set; }
        /* BHT Column group */
        public Nullable<decimal> DEAL_GUIDELINE { get; set; }
        public Nullable<decimal> NET_INTO_STORE { get; set; }
        public Nullable<decimal> AVERAGE_SALE { get; set; }
        /*CV column group*/
        public Nullable<decimal> AVG_VOLUMN { get; set; }
        public Nullable<decimal> SALE_QTY { get; set; }
        /* */
        public Nullable<decimal> SALE_VALUE { get; set; }
        public Nullable<decimal> SALE_UOM { get; set; }
        public string DISCOUNT_TYPE { get; set; }
        public string REMARK { get; set; }
        public DOCUMENT_STATUS DOC_STATUS { get; set; }
        public Nullable<DateTime> START_DATE { get; set; }
        public Nullable<DateTime> END_DATE { get; set; }
        public string FLAG_ROW { get; set; }

    }
    public class PromotionPlanningFooterDto
    {
        public string COMMENT { get; set; }
    }

}
