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
    
    public partial class ActorState
    {
        public int ActorStateId { get; set; }
        public int WorkflowStateId { get; set; }
        public int ActorId { get; set; }
        public string ContactType { get; set; }
        public string RoleState { get; set; }
        public string User { get; set; }
        public string Position { get; set; }
        public int OperatorConditionId { get; set; }
        public string OperatorCode { get; set; }
        public Nullable<decimal> LimitValue { get; set; }
    
        public virtual Actor Actor { get; set; }
        public virtual OperatorCondition OperatorCondition { get; set; }
        public virtual WorkflowState WorkflowState { get; set; }
    }
}
