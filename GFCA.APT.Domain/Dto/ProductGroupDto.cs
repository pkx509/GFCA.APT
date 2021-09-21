using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class ProductGroupDto : Auditable
    {
        [Required]
        public string PROGP_CODE { get; set; }
        public string PROD_CODE { get; set; }
        public Nullable<decimal> NORM_SHELF_PRICE { get; set; }
        public Nullable<decimal> NORM_DISCOUNT { get; set; }
        public Nullable<decimal> NORM_GP { get; set; }
        public string DISCOUNT_TYPE { get; set; }
        public string FLAG_ROW { get; set; }
    }
}
