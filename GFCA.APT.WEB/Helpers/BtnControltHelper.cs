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
            //Susess = 'S'
            //Error => 'E'

            string htmltxt = "";


            if (status == "D")
            {
                htmltxt = "<li class='nav-item mr-sm-1'><a href='#' id='btn-submit' class='btn btn-sm btn-default'>Submit</a></li>";
            }
            else if (string.IsNullOrEmpty(status))
            {
                htmltxt = "<li class='nav-item mr-sm-1'><a href='#' id='btn-submit' class='btn btn-sm btn-default'>Submit</a></li>";
            }

            else if (status == "D")
            {
                htmltxt = "<li class='nav-item mr-sm-1'><a href='#' id='btn-submit' class='btn btn-sm btn-success'>Submit</a></li>";
            }
            else if (status == "P")
            {
                htmltxt = "<li class='nav-item mr-sm-1'><a href='#' id='btn-approve' class='btn btn-sm btn-primary'>Approve</a></li>";
            }
            else
            {
                htmltxt = "";
            }



            return new MvcHtmlString(htmltxt.ToString());
        }



    }

}