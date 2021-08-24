using System;
using System.ComponentModel.DataAnnotations;
namespace GFCA.APT.Domain.Dto
{
    public class TB_M_ORGANIZATIONDto
    {
        public int ORG_ID { get; set; }
        public int? COMP_ID { get; set; }
        public string ORG_CODE { get; set; }
        public string ORG_TYPE { get; set; }
        public string ORG_DEPARTMENT_NAME { get; set; }
        public string ORG_POSITION_NAME { get; set; }
        public string ORG_DESC { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
