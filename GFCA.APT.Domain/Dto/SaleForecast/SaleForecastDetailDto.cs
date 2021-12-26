using GFCA.APT.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class SaleForecastDetailDto : Auditable
    {
        //private Nullable<decimal> _totalSales;
        //private Nullable<decimal> _totalFOC;
        
        [Required]
        public int DOC_SFCH_ID { get; set; }
        [Required]
        public int DOC_SFCD_ID { get; set; }
        [Required]
        public string DOC_CODE { get; set; }
        public int? DOC_VER { get; set; }
        public int? DOC_REV { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        //public string ACTIVITY_CODE { get; set; }
        //public string ACTIVITY_NAME { get; set; }
        //public string CENTER_CODE { get; set; }
        //public string CENTER_NAME { get; set; }
        //public string ACC_CODE { get; set; }
        //public string ACC_NAME { get; set; }
        public string SIZE { get; set; }
        public string UOM { get; set; }
        public string PACK { get; set; }
        public string PACK_NAME { get; set; }
        public string PROD_CODE { get; set; }
        public string PROD_NAME { get; set; }
        public int? YEAR { get; set; }
        //public string DATE_REF { get; set; }
        //[Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        //public CONDITION_TYPE CONDITION_TYPE { get; set; } = CONDITION_TYPE.NONE;
        //public string CONTRACT_CATE { get; set; }
        //public string CONTRACT_DESC { get; set; }
        public Nullable<decimal> M1Sales { get; set; }
        public Nullable<decimal> M1FOC { get; set; }
        public Nullable<decimal> M2Sales { get; set; }
        public Nullable<decimal> M2FOC { get; set; }
        public Nullable<decimal> M3Sales { get; set; }
        public Nullable<decimal> M3FOC { get; set; }
        public Nullable<decimal> M4Sales { get; set; }
        public Nullable<decimal> M4FOC { get; set; }
        public Nullable<decimal> M5Sales { get; set; }
        public Nullable<decimal> M5FOC { get; set; }
        public Nullable<decimal> M6Sales { get; set; }
        public Nullable<decimal> M6FOC { get; set; }
        public Nullable<decimal> M7Sales { get; set; }
        public Nullable<decimal> M7FOC { get; set; }
        public Nullable<decimal> M8Sales { get; set; }
        public Nullable<decimal> M8FOC { get; set; }
        public Nullable<decimal> M9Sales { get; set; }
        public Nullable<decimal> M9FOC { get; set; }
        public Nullable<decimal> M10Sales { get; set; }
        public Nullable<decimal> M10FOC { get; set; }
        public Nullable<decimal> M11Sales { get; set; }
        public Nullable<decimal> M11FOC { get; set; }
        public Nullable<decimal> M12Sales { get; set; }
        public Nullable<decimal> M12FOC { get; set; }
        //public Nullable<decimal> TotalSales { get; set; }
        //public Nullable<decimal> TotalFOC { get; set; }
        public Nullable<decimal> TotalSales {
            get { return M1Sales ?? 0 + M2Sales ?? 0 + M3Sales ?? 0 + M4Sales ?? 0 + M5Sales ?? 0 + M6Sales ?? 0 + M7Sales ?? 0 + M8Sales ?? 0 + M9Sales ?? 0 + M10Sales ?? 0 + M11Sales ?? 0 + M12Sales ?? 0; }
            //set { _totalSales = value; } 
        }
        public Nullable<decimal> TotalFOC
        {
            get { return M1FOC ?? 0 + M2FOC ?? 0 + M3FOC ?? 0 + M4FOC ?? 0 + M5FOC ?? 0 + M6FOC ?? 0 + M7FOC ?? 0 + M8FOC ?? 0 + M9FOC ?? 0 + M10FOC ?? 0 + M11FOC ?? 0 + M12FOC ?? 0; }
            //set { _totalFOC = value; }
        }
        public DOCUMENT_STATUS DOC_STATUS { get; set; }
        //public string REMARK { get; set; }
        //[Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        //public DOCUMENT_STATUS DOC_STATUS { get; set; } = DOCUMENT_STATUS.NONE;
        //public Nullable<DateTime> START_DATE { get; set; }
        //public Nullable<DateTime> END_DATE { get; set; }
        //[Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        //public ROW_TYPE FLAG_ROW { get; set; } = ROW_TYPE.SHOW;

    }
}
