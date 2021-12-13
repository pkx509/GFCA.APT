using GFCA.APT.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class BudgetPlanInvestmentDto : Auditable
    {

        //Detail 
  
        public long DOC_BGH_INV_ID { get; set; }
        public string BRAND_CODE { get; set; }
        public string PACK_CODE { get; set; }
        public string SIZE_CODE { get; set; }
        public string PRD_CODE { get; set; }
        public string ACTIVITY_CODE { get; set; }
        public string COST_ELEMENT_CODE { get; set; }
        public string COST_CENTER { get; set; }
        public int YEAR { get; set; }
        public int MONTH { get; set; }
        public decimal TOTAL { get; set; }
        public decimal M1 { get; set; }
        public decimal M2 { get; set; }
        public decimal M3 { get; set; }
        public decimal M4 { get; set; }
        public decimal M5 { get; set; }
        public decimal M6 { get; set; }
        public decimal M7 { get; set; }
        public decimal M8 { get; set; }
        public decimal M9 { get; set; }
        public decimal M10 { get; set; }
        public decimal M11 { get; set; }
        public decimal M12 { get; set; }

        // Header 

        public int DOC_BGH_ID { get; set; }
        public string DOC_CODE { get; set; }
        public int? DOC_VER { get; set; }
        public int? DOC_REV { get; set; }
        public string BG_TYPE_CODE { get; set; }
        public string COMP_CODE { get; set; }
        public string CUST_CODE { get; set; }
        public int? FISCAL_YEAR { get; set; }
        public decimal BUDGET_AMOUNT { get; set; }
        public decimal ACTUAL { get; set; }
        public decimal COMMITMENT { get; set; }
        public decimal AVAILABLE { get; set; }
        public ROW_TYPE FLAG_ROW { get; set; } = ROW_TYPE.SHOW;
 
        public string FLOW_PREVIOUS { get; set; }
        public string FLOW_CURRENT { get; set; }
        public string FLOW_NEXT { get; set; }
        public string DOC_STATUS { get; set; }

    }
}
