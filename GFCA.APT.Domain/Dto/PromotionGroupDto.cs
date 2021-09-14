using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class PromotionGroupDto : Auditable
    {
        [Required]
        public int PROGP_ID { get; set; }
        [Required]
        public int CHANNEL_ID { get; set; }
        [Required]
        public int CUST_ID { get; set; }
        [Required]
        public int CLIENT_ID { get; set; }
        public string PROGP_CODE { get; set; }
        public string PROGP_NAME { get; set; }
        public string PROGP_DESC { get; set; }


        public string CHANNEL_CODE { get; set; }
        public string CUST_CODE { get; set; }
        public string CLIENT_CODE { get; set; }



        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
 
    }
}
