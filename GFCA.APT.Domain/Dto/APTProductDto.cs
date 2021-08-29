using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto

{
    public class APTProduct
    {
        public string MaterialCode { get; set; }
        public string Description { get; set; }
        public string Division { get; set; }
        public string DivisionName { get; set; }
        public string SaleOrg { get; set; }
        public string DistributionChannel { get; set; }
        public string MatGroup { get; set; }
        public string MatGroupDesc { get; set; }
        public string MatGroup1 { get; set; }
        public string MatGroup1_Desc { get; set; }
        public string MatGroup2 { get; set; }
        public string MatGroup2_Desc { get; set; }
        public string MatGroup3 { get; set; }
        public string MatGroup3_Desc { get; set; }
        public string MatGroup4 { get; set; }
        public string MatGroup4_Desc { get; set; }
        public string Formula { get; set; }
        public decimal? Pack { get; set; }
        public string PackDetail { get; set; }
        public decimal? Size { get; set; }
        public string SizeUOM { get; set; }
        public string SalesUOM { get; set; }
        public decimal? Conv_FCL { get; set; }
        public decimal? Conv_L { get; set; }
    }
}
