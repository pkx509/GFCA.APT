using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GFCA.APT.WEB.CustomAttributes
{
    // https://github.com/aspnet/AspNetWebStack/blob/main/src/System.Web.Mvc/Routing/RouteDataTokenKeys.cs
    internal class RouteDataTokenKeys
    {
        public const string UseNamespaceFallback = "UseNamespaceFallback";
        public const string Namespaces = "Namespaces";
        public const string Area = "area";
        public const string Controller = "controller";
        public const string Action = "action";


        // Used to provide the action descriptors to consider for attribute routing
        public const string Actions = "MS_DirectRouteActions";

        // Used to allow customer-provided disambiguation between multiple matching attribute routes
        public const string Order = "MS_DirectRouteOrder";

        // Used to prioritize routes to actions for link generation
        public const string TargetIsAction = "MS_DirectRouteTargetIsAction";

        // Used to allow URI constraint-based disambiguation between multiple matching attribute routes
        public const string Precedence = "MS_DirectRoutePrecedence";

        public const string DirectRouteMatches = "MS_DirectRouteMatches";
    }
}