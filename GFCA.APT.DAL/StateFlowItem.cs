//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GFCA.APT.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class StateFlowItem
    {
        public int StateFlowItemId { get; set; }
        public int WorkflowStateId { get; set; }
        public int FlowItemId { get; set; }
        public string FlowItemCode { get; set; }
        public string FlowItemName { get; set; }
        public int Sort { get; set; }
    
        public virtual FlowItem FlowItem { get; set; }
        public virtual WorkflowState WorkflowState { get; set; }
    }
}
