﻿using GFCA.APT.WEB.CustomAttributes;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new LogActionFilter());
        }
    }
}
