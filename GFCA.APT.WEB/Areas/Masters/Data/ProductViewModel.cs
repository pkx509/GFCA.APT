using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.WEB.Areas.Masters.Data
{
    public class ProductViewModel
    {
        [Required]
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public int BrandId { get; set; }
        public string BrandCode { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Color { get; set; }
        public Nullable<decimal> Width { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Height { get; set; }
        public string Shape { get; set; }
        public string Size { get; set; }
        public Nullable<int> QTY { get; set; }
        public string UOM { get; set; }
        public Nullable<decimal> LTP { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

    }
}