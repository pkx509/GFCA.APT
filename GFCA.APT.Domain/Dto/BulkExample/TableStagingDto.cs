using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class TableStagingDto
    {
        public int ROW_INDEX { get; set; }
        public string PROD_CODE { get; set; }
        public int FISCAL_YEAR { get; set; }
        public int FISCAL_MONTH { get; set; }
        public double BUDGET_AMOUNT { get; set; }

        public string UPLOAD_BY { get; set; }
        public DateTime? UPDATE_DATE { get; set; }

    }
}
