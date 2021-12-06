using GFCA.APT.Domain.Enums;
using System;

namespace GFCA.APT.Domain.Dto
{
    public class PromotionPlanngOverviewDto : Auditable
    {
        public string DOC_TYPE_CODE { get; set; }

        public int? DOC_PROM_PH_ID { get; set; } = 0;

        public string DOC_CODE { get; set; }
        public int? DOC_VER { get; set; } = 0;
        public int? DOC_REV { get; set; } = 0;

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DOCUMENT_STATUS DOC_STATUS { get; set; } = DOCUMENT_STATUS.NONE;

        public string PROMO_NAME { get; set; }
        public DateTime PROMO_START { get; set; }  = DateTime.Today;
        public DateTime PROMO_END { get; set; }    = new DateTime(2099, 12, 31);
        public DateTime BUYING_START { get; set; } = DateTime.Today;
        public DateTime BUYING_END { get; set; }   = new DateTime(2099, 12, 31);

        public string CLIENT_CODE { get; set; }
        public string CLIENT_NAME { get; set; }

        public string CHANNEL_CODE { get; set; }
        public string CHANNEL_NAME { get; set; }

        public string CUST_CODE { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_SEGMENT { get; set; }

        public decimal TOTAL_EST_SALE { get; set; }   = 0.00M;
        public decimal TOTAL_EST_INVEST { get; set; } = 0.00M;
        public decimal SALE_VS_INVEST { get; set; }   = 0.00M;

        public string COMMENT { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ROW_TYPE FLAG_ROW { get; set; } = ROW_TYPE.SHOW;
    }
}
