using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class CustomerDto
    {
        [Required]
        public int CUSTOMER_ID { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_NAME { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
    }
}
