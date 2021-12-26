using System;

namespace GFCA.APT.Domain.Dto.Workflow
{
    public class CommandDto
    {
        public int WF_STATE_ID { get; set; }
        public int FLOW_ITEM_ID { get; set; }
        public string STATE_CODE { get; set; }
        public string FLOW_ITEM_CODE { get; set; }
        public string FLOW_ITEM_NAME { get; set; }
        public string FLOW_ITEM_DESC { get; set; }
        public string DIRECTION_CODE { get; set; }
        public string DIRECTION_NAME { get; set; }
        public int Sort { get; set; }

    }
}
