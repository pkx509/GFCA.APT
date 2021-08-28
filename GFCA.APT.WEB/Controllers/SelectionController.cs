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
            IBusinessProvider biz = new BusinessProvider();
            //IBrandService svc = BrandService.CreateInstant();
            IEmissionService svc = biz.EmissionService;
            //IEmissionService svc = EmissionService.CreateInstant();
            var ret = svc.GetAll()
                .Where(o => (o.FLAG_ROW == null) || o.FLAG_ROW == FLAG_ROW.SHOW)
                .Select(o => new SelectionItem { Value = o.EMIS_ID, Text = $"{o.EMIS_CODE} - {o.EMIS_NAME}" });

            return ret;
        }


        // POST: api/Selection/GetBrand
        [HttpPost]
        public IEnumerable<SelectionItem> GetBrand()
        {
            IBusinessProvider biz = new BusinessProvider();
            //IBrandService svc = BrandService.CreateInstant();
            IBrandService svc = biz.BrandService;
            var ret = svc.GetAll()
                .Where(o => (o.FLAG_ROW == null) || o.FLAG_ROW == FLAG_ROW.SHOW)
                .Select(o => new SelectionItem { Value = o.BRAND_ID, Text = $"{o.BRAND_CODE} - {o.BRAND_NAME}" });

            return ret;
        }

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



        [HttpPost]
        public IEnumerable<SelectionItem> GetCustomer()
        {
            IBusinessProvider biz = new BusinessProvider();
            IProductService svc = biz.ProductService;
            var ret = svc.GetCustomer().Select(o => new SelectionItem { Value = o.CUST_CODE, Text = $"{o.CUST_CODE} - {o.CUST_NAME}" });
            return ret;
        }
        //GetCustomer

    }
}
