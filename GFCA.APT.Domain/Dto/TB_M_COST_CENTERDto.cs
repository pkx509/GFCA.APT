using System;
using System.ComponentModel.DataAnnotations;
namespace GFCA.APT.Domain.Dto
{
    public class TB_M_COST_CENTERDto
    {
        public int CENTER_ID { get; set; }
        public string CENTER_CODE { get; set; }
        public string CENTER_NAME { get; set; }
        public string CENTER_DESC { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
