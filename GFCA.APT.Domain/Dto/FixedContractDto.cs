using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class FixedContractDto : Auditable
    {

        public FixedContractHeaderDto Header { get; set; }
        public FixedContractDetailDto Detail { get; set; }
        public IEnumerable<FixedContractFooter> Footer { get; set; }
    }

    public class FixedContractHeaderDto : Auditable
    {
        /* Document */
        public string DOC_TYPE_CODE { get; set; }
        public string DOC_CODE { get; set; }
        public string DOC_VER { get; set; }
        public string DOC_REV { get; set; }
        public string DOC_MONTH { get; set; }
        public string DOC_YEAR { get; set; }
        public string DOC_STATUS { get; set; }
        public string FLOW_CURRENT { get; set; }
        public string FLOW_NEXT { get; set; }
        public string REQUESTER { get; set; }

        /* Fixed contract header */
        public int DOC_FCH_ID { get; set; }
        //public string DOC_CODE { get; set; }
        public string CLIENT_CODE { get; set; }
        public string CUST_CODE { get; set; }
        public string CHANNEL_CODE { get; set; }
        public string FLAG_ROW { get; set; }
    }

    public class FixedContractDetailDto : Auditable
    {
        public int DOC_FCH_ID { get; set; }
        public int DOC_FCD_ID { get; set; }
        public string DOC_CODE { get; set; }
        public string BRAND_CODE { get; set; }
        public string ACTIVITY_CODE { get; set; }
        public string CENTER_CODE { get; set; }
        public string ACC_CODE { get; set; }
        public string SIZE { get; set; }
        public string UOM { get; set; }
        public string PACK { get; set; }
        public string DATE_REF { get; set; }
        public string CONDITION_TYPE { get; set; }
        public string CONTRACT_CATE { get; set; }
        public string CONTRACT_DESC { get; set; }
        public Nullable<decimal> M01 { get; set; }
        public Nullable<decimal> M02 { get; set; }
        public Nullable<decimal> M03 { get; set; }
        public Nullable<decimal> M04 { get; set; }
        public Nullable<decimal> M05 { get; set; }
        public Nullable<decimal> M06 { get; set; }
        public Nullable<decimal> M07 { get; set; }
        public Nullable<decimal> M08 { get; set; }
        public Nullable<decimal> M09 { get; set; }
        public Nullable<decimal> M10 { get; set; }
        public Nullable<decimal> M11 { get; set; }
        public Nullable<decimal> M12 { get; set; }
        public string COMMENT { get; set; }
        public string FLAG_ROW { get; set; }

    }
    public class FixedContractFooter
    {
        public string COMMENT { get; set; }
    }
}
