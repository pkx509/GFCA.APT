using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class BrandDto : Auditable
    {
        [Required]
        public int? BRAND_ID { get; set; }
        public int? CLIENT_ID { get; set; }
        public string CLIENT_CODE { get; set; }
        [Required]
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string BRAND_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }

        /*
        public string FLAG_ROW
        { 
            get 
            {
                var flagRow = GFCA.APT.Domain.Enums.FLAG_ROW.SHOW;
                if (!IS_ACTIVED)
                    flagRow = GFCA.APT.Domain.Enums.FLAG_ROW.DELETE;
                return flagRow;
            } 
        }
        */
        /* อยู่ใน class Auditable แล้ว
         *
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        */
    }
}
