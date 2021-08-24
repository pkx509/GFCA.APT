using System;
using System.ComponentModel.DataAnnotations;
namespace GFCA.APT.Domain.Dto
{
    public class TB_M_UNITDto
    {
        public int UNIT_ID { get; set; }
        public int? PARENT_ID { get; set; }
        public string UNIT_CODE { get; set; }
        public string UNIT_NAME { get; set; }
        public string UNIT_TYPE { get; set; }
        public decimal FACTOR { get; set; }
        public string UNIT_DESC { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
