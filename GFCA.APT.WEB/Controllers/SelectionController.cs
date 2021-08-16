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
        [HttpPost]
        public IEnumerable<SelectionItem> GetEmission()
        {
            IEmissionService svc = EmissionService.CreateInstant();
            var ret = svc.GetAll()
                .Where(o => (o.FLAG_ROW == null) || o.FLAG_ROW == FLAG_ROW.SHOW)
                .Select(o => new SelectionItem { Value = o.EMIS_ID, Text = $"{o.EMIS_CODE} - {o.EMIS_NAME}" });

            return ret;
        }


        // POST: api/Selection/GetBrand
        [HttpPost]
        public IEnumerable<SelectionItem> GetBrand()
        {
            IBrandService svc = BrandService.CreateInstant();
            var ret = svc.GetAll()
                .Where(o => (o.FLAG_ROW == null) || o.FLAG_ROW == FLAG_ROW.SHOW)
                .Select(o => new SelectionItem { Value = o.BRAND_ID, Text = $"{o.BRAND_CODE} - {o.BRAND_NAME}" });

            return ret;
        }
    }
}
