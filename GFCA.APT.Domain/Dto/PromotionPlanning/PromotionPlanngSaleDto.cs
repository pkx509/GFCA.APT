namespace GFCA.APT.Domain.Dto
{
    public class PromotionPlanngSaleDto : Auditable
    {
        public int DOC_PROM_PS_ID { get; set; }

        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }

        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }

        public string PROD_CODE { get; set; }
        public string PROD_SKU { get; set; }
        public double PROD_LTP_EXCL_VAT { get; set; }
        
        public double NORM_PERC_DISC { get; set; }
        public double NORM_PERC_GP { get; set; }
        public double NORM_SHELF_PRICE { get; set; }

        public double PROMO_PERC_DISC { get; set; }
        public double PROMO_PERC_GP { get; set; }
        public double PROMO_SHELF_PRICE { get; set; }

        public double DEAL_GUIDE_LINE { get; set; }
        public double NET_INTO_STORE { get; set; }

        public double AVG_SALE { get; set; }
        public double AVG_VOLUME { get; set; }

        public double SALE_QTY { get; set; }
        public double SALE_VALUE_EXCL_VAT { get; set; }
        public string SALE_UOM { get; set; }

        public string DISC_TYPE { get; set; }

    }
}
