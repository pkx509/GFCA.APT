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
        public int PROGP_ID { get; set; }
        [Required]
        public int PROD_ID { get; set; }
        public decimal? NORM_SHELF_PRICE { get; set; }
        public decimal? NORM_DISCOUNT { get; set; }
        public decimal? NORM_GP { get; set; }
        public string DISCOUNT_TYPE { get; set; }
        public string FLAG_ROW { get; set; }
    }
}
