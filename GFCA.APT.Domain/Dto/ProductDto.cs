using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class ProductDto
    {
        [Required]
        public int PROD_ID { get; set; }
        public string PROD_CODE { get; set; }
        public string PROD_NAME { get; set; }
        public string CUST_CODE { get; set; }
        public string MAT_CODE { get; set; }
        public string ORG_CODE { get; set; }
        public string DIV_CODE { get; set; }
        public string EMIS_CODE { get; set; }
        public string MAT_GROUP { get; set; }
        public string MAT_GROUP_DESC { get; set; }
        public string MAT_GROUP1 { get; set; }
        public string MAT_GROUP1_DESC { get; set; }
        public string MAT_GROUP2 { get; set; }
        public string MAT_GROUP2_DESC { get; set; }
        public string MAT_GROUP3 { get; set; }
        public string MAT_GROUP3_DESC { get; set; }
        public string FORMULA { get; set; }
        public string PACK { get; set; }
        public string PACK_DESC { get; set; }
        public string UNIT_CODE { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
    }
}
