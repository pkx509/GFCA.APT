using System;
using System.ComponentModel.DataAnnotations;
namespace GFCA.APT.Domain.Dto
{
    public class TB_M_PROMOTION_GROUPDto
    {
        public int PROGP_ID { get; set; }
        public int CHANNEL_ID { get; set; }
        public int CUST_ID { get; set; }
        public int CLIENT_ID { get; set; }
        public string PROGP_CODE { get; set; }
        public string PROGP_NAME { get; set; }
        public string PROGP_DESC { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
