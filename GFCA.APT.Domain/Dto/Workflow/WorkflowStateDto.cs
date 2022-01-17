using System;

namespace GFCA.APT.Domain.Dto
{
    public class WorkflowStateDto
    {
        public int WF_STATE_ID { get; set; }
        public int? WF_STATE_PARENT_ID { get; set; }
        public string STATE_CODE { get; set; }
        public string STATE_NAME { get; set; }
        public string STATE_DESC { get; set; }
        public string NOTI_SUBJECT { get; set; }
        public string NOTI_MESSAGE { get; set; }
        public string NOTI_FOOTER { get; set; }
        public DateTime EFF_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public string DOC_TYPE_CODE { get; set; }
        public string DOC_TYPE_NAME { get; set; }
        public int FLOW_ITEM_ID { get; set; }
        public string FLOW_ITEM_CODE { get; set; }
        public string DIRECTION_CODE { get; set; }
        public string DIRECTION_NAME { get; set; }
        public int SORT { get; set; }
    }
}
