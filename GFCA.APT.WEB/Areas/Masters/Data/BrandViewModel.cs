using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.WEB.Areas.Masters.Data
{
    public class BrandViewModel
    {
        [Required]
        public int? BrandId { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string UpdatedBy { get; set; }
        //public Nullable<DateTime> UpdatedDate { get; set; }
    }
}