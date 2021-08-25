using GFCA.APT.BAL.Implements;
using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.HTTP.Controls;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace GFCA.APT.WEB.Controllers
{
    public class SelectionController : ApiController
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // POST: api/Selection/GetEmission
        // POST: api/Selection/GetEmission/true
        [HttpPost]
        public IEnumerable<SelectionItem> GetEmission(bool isOption = false)
        {
            IBusinessProvider biz = new BusinessProvider();
            IEmissionService svc = biz.EmissionService;
            var ret = svc.GetAll()
                .Where(o => (o.FLAG_ROW == null) || o.FLAG_ROW == FLAG_ROW.SHOW)
                .Select(o => new SelectionItem { Value = o.EMIS_ID, Text = $"{o.EMIS_CODE} - {o.EMIS_NAME}" })
                .ToList();

            if (isOption)
            {
                ret.Insert(0, new SelectionItem() { Value = null, Text = "-- Select --" });
            }

            return ret;
        }


        // POST: api/Selection/GetBrand
        // POST: api/Selection/GetBrand/true
        [HttpPost]
        public IEnumerable<SelectionItem> GetBrand(bool isOption = false)
        {
            IBusinessProvider biz = new BusinessProvider();
            IBrandService svc = biz.BrandService;
            var ret = svc.GetAll()
                .Where(o => (o.FLAG_ROW == null) || o.FLAG_ROW == FLAG_ROW.SHOW)
                .Select(o => new SelectionItem { Value = o.BRAND_ID, Text = $"{o.BRAND_CODE} - {o.BRAND_NAME}" })
                .ToList();

            if (isOption)
            {
                ret.Insert(0, new SelectionItem() { Value = null, Text = "-- Select --" });
            }
            return ret;
        }
    }
}
