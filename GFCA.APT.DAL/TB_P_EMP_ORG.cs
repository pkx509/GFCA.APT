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
    
    public partial class TB_P_EMP_ORG
    {
        public int EMP_ID { get; set; }
        public int ORG_ID { get; set; }
        public string ORG_TYPE { get; set; }
        public string EMP_CODE { get; set; }
        public string ORG_CODE { get; set; }
        public Nullable<System.DateTime> EFF_FROM { get; set; }
        public Nullable<System.DateTime> EFF_TO { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
    
        public virtual TB_M_EMPLOYEE TB_M_EMPLOYEE { get; set; }
        public virtual TB_M_ORGANIZATION TB_M_ORGANIZATION { get; set; }
    }
}
