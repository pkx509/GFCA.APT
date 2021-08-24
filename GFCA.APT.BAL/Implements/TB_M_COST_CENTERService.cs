using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Implements
{
    public class TB_M_COST_CENTERService : ServiceBase, ITB_M_COST_CENTERService
    {
        public static TB_M_COST_CENTERService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new TB_M_COST_CENTERService(uow);
            return svc;
        }

        public IEnumerable<TB_M_COST_CENTERDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public TB_M_COST_CENTERDto GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Create(TB_M_COST_CENTERDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Edit(TB_M_COST_CENTERDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Remove(TB_M_COST_CENTERDto model)
        {
            throw new NotImplementedException();
        }

        public TB_M_COST_CENTERService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
