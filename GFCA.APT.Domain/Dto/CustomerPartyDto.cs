using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class CustomerPartyDto : Auditable
    {
        [Required]
        public int? PARTY_ID { get; set; }
        [Required]
        public int? CUST_ID { get; set; }
        [Required]
        public int? ACC_ID { get; set; }
        [Required]
        public int? DISTB_ID { get; set; }
        [Required]
        public int? CHANNEL_ID { get; set; }
        [Required]
        public int? VENDOR_ID { get; set; }
        [Required]
        public string CUST_CODE { get; set; }
        [Required]
        public string ACC_CODE { get; set; }
        [Required]
        public string DISTB_CODE { get; set; }
        [Required]
        public string CHANNEL_CODE { get; set; }
        [Required]
        public string VENDOR_CODE { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
