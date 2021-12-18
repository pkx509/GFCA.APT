using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Helpers
{

    public class BtnControltHelper
    {


        public static IHtmlString btnHtml(string Approver, string status = null)
        {

            //Create = 'D'
            //Submit == > 'P'
            //Approve == > 'W'
            //Success = 'S'
            //Error => 'E'

            /*
              [EnumMember(Value = "NONE")] NONE = 0,
        [EnumMember(Value = "DRAFT")] DRAFT = 1,
        [EnumMember(Value = "APPROVAL")] APPROVAL = 2,
        [EnumMember(Value = "REVIEW")] REVIEW = 3,
        [EnumMember(Value = "COMPLETED")] COMPLETED = 4,
        [EnumMember(Value = "REVISED")] REVISED = 5,
        [EnumMember(Value = "CANCELLED")] CANCELLED = -1,

             
             */

            string htmltxt = "";


            if (status == "DRAFT")
            {
                htmltxt = "<li class='nav-item mr-sm-1'><a href='#' id='btn-submit' class='btn btn-sm btn-default'>Submit</a></li>";
            }
            else if (string.IsNullOrEmpty(status))
            {
                htmltxt = "<li class='nav-item mr-sm-1'><a href='#' id='btn-submit' class='btn btn-sm btn-default'>Submit</a></li>";
            }

            else if (status == "APPROVAL")
            {
                htmltxt = "<li class='nav-item mr-sm-1'><a href='#' id='btn-approve' class='btn btn-sm btn-success'>Approve</a></li>";
            }
            else
            {
                htmltxt = "";
            }



            return new MvcHtmlString(htmltxt.ToString());
        }



    }

}