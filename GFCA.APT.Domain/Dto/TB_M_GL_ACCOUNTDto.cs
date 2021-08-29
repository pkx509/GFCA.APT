using System;
using System.ComponentModel.DataAnnotations;
namespace GFCA.APT.Domain.Dto
{
    public class TB_M_GL_ACCOUNTDto
    {
        public int ACC_ID { get; set; }
        public int? IO_ID { get; set; }
        public int? CENTER_ID { get; set; }
        public int? FUND_ID { get; set; }
        public int? FUND_CENTER_ID { get; set; }
        public string ACC_CODE { get; set; }
        public string ACC_NAME { get; set; }
        public string ACC_TYPE { get; set; }
        public string ACC_TYPE_DESC { get; set; }
        public string ACC_GROUP1 { get; set; }
        public string ACC_GROUP1_DESC { get; set; }
        public string ACC_GROUP2 { get; set; }
        public string ACC_GROUP2_DESC { get; set; }
        public string ACC_REMARK { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
