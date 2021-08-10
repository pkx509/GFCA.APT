using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class BrandDto
    {
        [Required]
        public int? BRAND_ID { get; set; }
        [Required]
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<DateTime> UPDATED_DATE { get; set; }
    }
}
