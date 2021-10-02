using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class ProductDto : Auditable
    {

        [Required]
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
        public string MAT_GROUP4 { get; set; }
        public string MAT_GROUP4_DESC { get; set; }
        public string FORMULA { get; set; }
        public Nullable<int> PACK { get; set; }

        public string PACK_DESC { get; set; }
        public Nullable<int> SIZE { get; set; }
        public string UOM_SIZE { get; set; }
        public string UOM_SALE { get; set; }
        public string UNIT_CODE { get; set; }
        public Nullable<decimal> CONV_FCL { get; set; }
        public Nullable<decimal> CONV_L { get; set; }
        public string FLAG_ROW { get; set; }

        public string CUST_NAME { get; set; }
        public string ORG_NAME { get; set; }
        public string EMIS_NAME { get; set; }

        public string CUST_CODE_NAME { get; set; }
        public string ORG_CODE_NAME { get; set; }
        public string EMIS_CODE_NAME { get; set; }

    }
}
