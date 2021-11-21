using System;

namespace GFCA.APT.Domain.Dto
{
    public class PromotionPlanngOverviewDto
    {
        public string DOC_CODE { get; set; }

        public string PROMO_NAME { get; set; }
        public DateTime PROMO_START { get; set; } = DateTime.Today;
        public DateTime PROMO_END { get; set; } = new DateTime(2099, 12, 31);
        public DateTime BUYING_START { get; set; } = DateTime.Today;
        public DateTime BUYING_END { get; set; } = new DateTime(2099, 12, 31);

        public string CLIENT_CODE { get; set; }
        public string CLIENT_NAME { get; set; }

        public string CHANNEL_CODE { get; set; }
        public string CHANNEL_NAME { get; set; }

        public string CUST_CODE { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_SEGMENT { get; set; }

        public double TOTAL_EST_SALE { get; set; }
        public double TOTAL_EST_INVEST { get; set; }
        public double SALE_VS_INVEST { get; set; }

    }
}
