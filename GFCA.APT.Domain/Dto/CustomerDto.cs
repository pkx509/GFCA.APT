using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class CustomerDto : Auditable
    {
        [Required]
        public int? CUST_ID { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_ABV { get; set; }
        public string CUST_GROUP1 { get; set; }
        public string CUST_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
