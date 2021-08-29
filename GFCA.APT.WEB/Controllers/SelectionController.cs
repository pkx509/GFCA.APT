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

        // POST: api/Selection/GetTradeActivity
        // POST: api/Selection/GetTradeActivity/true
        [HttpPost]
        public IEnumerable<SelectionItem> GetTradeActivity(bool isOption = false)
        {
            IBusinessProvider biz = new BusinessProvider();
            ITradeActivityService svc = biz.TradeActivityService;
            var ret = svc.GetAll()
                .Where(o => (o.FLAG_ROW == null) || o.FLAG_ROW == FLAG_ROW.SHOW)
                .Select(o => new SelectionItem { Value = o.ACTIVITY_ID, Text = $"{o.ACTIVITY_CODE} - {o.ACTIVTITY_NAME}" })
                .ToList();

            if (isOption)
            {
                ret.Insert(0, new SelectionItem() { Value = null, Text = "-- Select --" });
            }
            return ret;
        }


        // POST: api/Selection/GetClient
        // POST: api/Selection/GetClient/true
        [HttpPost]
        public IEnumerable<SelectionItem> GetClient()
        {
            IBusinessProvider biz = new BusinessProvider();
            IClientService svc = biz.ClientService;
            var ret = svc.GetAll()
                .Where(o => (o.FLAG_ROW == null) || o.FLAG_ROW == FLAG_ROW.SHOW)
                .Select(o => new SelectionItem { Value = o.CLIENT_ID, Text = $"{o.CLIENT_CODE} - {o.CLIENT_NAME}" });

            return ret;
        }
        /*
        [HttpPost]
        public IEnumerable<SelectionItem> GetMatGroup()
        {
            IBusinessProvider biz = new BusinessProvider();
            IProductService svc = biz.ProductService;
            var ret = svc.GetMatGroup().Select(o => new SelectionItem { Value = o.MatGroup, Text = $"{o.MatGroup} - {o.MatGroupDesc}" });

        //    return null;
          return ret;
        }

        [HttpPost]
        public IEnumerable<SelectionItem> GetMatGroup1()
        {
            IBusinessProvider biz = new BusinessProvider();
            IProductService svc = biz.ProductService;
            var ret = svc.GetMatGroup1().Select(o => new SelectionItem { Value = o.MatGroup1, Text = $"{o.MatGroup1} - {o.MatGroup1_Desc}" });

            return ret;
        }


        [HttpPost]
        public IEnumerable<SelectionItem> GetMatGroup2()
        {
            IBusinessProvider biz = new BusinessProvider();
            IProductService svc = biz.ProductService;
            var ret = svc.GetMatGroup2().Select(o => new SelectionItem { Value = o.MatGroup2, Text = $"{o.MatGroup2} - {o.MatGroup2_Desc}" });

            return ret;
        }


        [HttpPost]
        public IEnumerable<SelectionItem> GetMatGroup3()
        {
            IBusinessProvider biz = new BusinessProvider();
            IProductService svc = biz.ProductService;
            var ret = svc.GetMatGroup3().Select(o => new SelectionItem { Value = o.MatGroup3, Text = $"{o.MatGroup3} - {o.MatGroup3_Desc}" });

            return ret;
        }



        [HttpPost]
        public IEnumerable<SelectionItem> GetMatGroup4()
        {
            IBusinessProvider biz = new BusinessProvider();
            IProductService svc = biz.ProductService;
            var ret = svc.GetMatGroup4().Select(o => new SelectionItem { Value = o.MatGroup4, Text = $"{o.MatGroup4} - {o.MatGroup4_Desc}" });
            return ret;
        }



        [HttpPost]
        public IEnumerable<SelectionItem> GetFormula()
        {
            IBusinessProvider biz = new BusinessProvider();
            IProductService svc = biz.ProductService;
            var ret = svc.GetFormula().Select(o => new SelectionItem { Value = o.Formula, Text = $"{o.Formula}" });
            return ret;
        }




        [HttpPost]
        public IEnumerable<SelectionItem> GetPack()
        {
            IBusinessProvider biz = new BusinessProvider();
            IProductService svc = biz.ProductService;
            var ret = svc.GetPack().Select(o => new SelectionItem { Value = o.Pack, Text = $"{o.Pack} - {o.PackDetail}" });
            return ret;
        }
        */

        // POST: api/Selection/GetCustomer
        // POST: api/Selection/GetCustomer/true
        [HttpPost]
        public IEnumerable<SelectionItem> GetCustomer(bool isOption = false)
        {
            IBusinessProvider biz = new BusinessProvider();
            ICustomerService svc = biz.CustomerService;
            var ret = svc.GetAll()
                .Where(o => (o.FLAG_ROW == null) || o.FLAG_ROW == FLAG_ROW.SHOW)
                .Select(o => new SelectionItem { Value = o.CUST_ID, Text = $"{o.CUST_CODE} - {o.CUST_NAME}" })
                .ToList();
            /*
            if (isOption)
            {
                ret.Insert(0, new SelectionItem() { Value = null, Text = "-- Select --" });
            }
            */
            return ret;
        }
        //GetCustomer

    }
}
