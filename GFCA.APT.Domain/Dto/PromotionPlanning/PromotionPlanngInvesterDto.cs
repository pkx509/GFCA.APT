using GFCA.APT.Domain.Enums;

namespace GFCA.APT.Domain.Dto
{
    public class PromotionPlanngInvesterDto : Auditable
    {
        public int DOC_PROM_PI_ID { get; set; } //PK
        public int DOC_PROM_PS_ID { get; set; } //FK

        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }

        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }

        public string PROD_CODE { get; set; }
        public string PROD_SKU { get; set; }

        public string ACTIVITY_CODE { get; set; }
        public string ACTIVITY_NAME { get; set; }

        public INVESTMENT_TYPE INVEST_TYPE { get; set; } = INVESTMENT_TYPE.VALUE; // PERCENT, VALUE
        public double INVEST_VALUE { get; set; } = 0.00;
        public double INVEST_AMOUNT { get; set; } = 0.00; 

        public string ACTIVITY_CODE_OTHER { get; set; }
        public string ACTIVITY_NAME_OTHER { get; set; }
        public string ACTIVITY_COMBINED_OTHER { get; set; } = "N"; //Y, N
        public double INCREMENT_SALE_INVEST { get; set; } = 0.00;

        public string INVEST_ACC_CODE { get; set; }
        public string INVEST_ACC_NAME { get; set; }

        public string FUND1_CODE { get; set; }
        public string FUND1_NAME { get; set; }
        public string FUND1_COST_CODE { get; set; }
        public string FUND1_COST_NAME { get; set; }
        public double FUND1_AMOUNT { get; set; } = 0.00;

        public string FUND2_CODE { get; set; }
        public string FUND2_NAME { get; set; }
        public string FUND2_COST_CODE { get; set; }
        public string FUND2_COST_NAME { get; set; }
        public double FUND2_AMOUNT { get; set; } = 0.00;

        public string REMARKS { get; set; }

    }
}
