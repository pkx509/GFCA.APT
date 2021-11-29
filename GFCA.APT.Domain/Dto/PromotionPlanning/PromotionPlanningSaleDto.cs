using GFCA.APT.Domain.Enums;

namespace GFCA.APT.Domain.Dto
{
    public class PromotionPlanningSaleDto : Auditable
    {
        public int DOC_PROM_PS_ID { get; set; } = 0; //PK
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
        public decimal PROD_LTP_EXCL_VAT { get; set; }
        
        public decimal NORM_PERC_DISC { get; set; }
        public decimal NORM_PERC_GP { get; set; }
        public decimal NORM_SHELF_PRICE { get; set; }

        public decimal PROMO_PERC_DISC { get; set; }
        public decimal PROMO_PERC_GP { get; set; }
        public decimal PROMO_SHELF_PRICE { get; set; }

        public decimal DEAL_GUIDE_LINE { get; set; }
        public decimal NET_INTO_STORE { get; set; }

        public decimal AVG_SALE { get; set; }
        public decimal AVG_VOLUME { get; set; }

        public int SALE_QTY { get; set; }
        public decimal SALE_VALUE_EXCL_VAT { get; set; }
        public string SALE_UOM { get; set; }

        public string DISC_TYPE { get; set; }

        public ROW_TYPE FLAG_ROW { get; set; } = ROW_TYPE.SHOW;

    }
}
