using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class TableMasterDto
    {
        public int ROW_INDEX { get; set; }
        public string PROD_CODE { get; set; }
        public int FISCAL_YEAR { get; set; }
        public int FISCAL_MONTH { get; set; }
        public double BUDGET_AMOUNT { get; set; }

        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
