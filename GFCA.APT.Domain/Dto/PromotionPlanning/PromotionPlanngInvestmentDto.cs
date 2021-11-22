using GFCA.APT.Domain.Enums;

namespace GFCA.APT.Domain.Dto
{
    public class PromotionPlanngInvestmentDto : Auditable
    {
        public int DOC_PROM_PI_ID { get; set; } = 0; //PK
        public int? DOC_PROM_PS_ID { get; set; } //FK
        public int? DOC_PROM_PH_ID { get; set; } //FK

        public string DOC_CODE { get; set; }
        public string DOC_VER { get; set; }
        public string DOC_REV { get; set; }

        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }

        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }

        public string PROD_CODE { get; set; }
        public string PROD_SKU { get; set; }

        public string ACTIVITY_CODE { get; set; }
        public string ACTIVITY_NAME { get; set; }

        public INVESTMENT_TYPE INVEST_TYPE { get; set; } = INVESTMENT_TYPE.VALUE; // PERCENT, VALUE
        public decimal INVEST_VALUE { get; set; } = 0.00M;
        public decimal INVEST_AMOUNT { get; set; } = 0.00M; 

        public string ACTIVITY_CODE_OTHER { get; set; }
        public string ACTIVITY_NAME_OTHER { get; set; }
        public string ACTIVITY_COMBINED_OTHER { get; set; } = "N"; //Y, N
        public decimal INCREMENT_SALE_INVEST { get; set; } = 0.00M;

        public string INVEST_ACC_CODE { get; set; }
        public string INVEST_ACC_NAME { get; set; }

        public string FUND1_CODE { get; set; }
        public string FUND1_NAME { get; set; }
        public string FUND1_CENTER_CODE { get; set; }
        public string FUND1_CENTER_NAME { get; set; }
        public decimal FUND1_AMOUNT { get; set; } = 0.00M;

        public string FUND2_CODE { get; set; }
        public string FUND2_NAME { get; set; }
        public string FUND2_CENTER_CODE { get; set; }
        public string FUND2_CENTER_NAME { get; set; }
        public decimal FUND2_AMOUNT { get; set; } = 0.00M;

        public string REMARKS { get; set; }
        public ROW_TYPE FLAG_ROW { get; set; } = ROW_TYPE.SHOW;

    }
}
