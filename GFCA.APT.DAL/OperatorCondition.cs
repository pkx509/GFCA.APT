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
    
    public partial class OperatorCondition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OperatorCondition()
        {
            this.ActorStates = new HashSet<ActorState>();
        }
    
        public int OperatorConditionId { get; set; }
        public string OperatorCode { get; set; }
        public string OperatoDesc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActorState> ActorStates { get; set; }
    }
}
