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
    public class TB_M_CUSTOMERService : ServiceBase, ITB_M_CUSTOMERService
    {
        public static TB_M_CUSTOMERService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new TB_M_CUSTOMERService(uow);
            return svc;
        }

        public IEnumerable<TB_M_CUSTOMERDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public TB_M_CUSTOMERDto GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Create(TB_M_CUSTOMERDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Edit(TB_M_CUSTOMERDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Remove(TB_M_CUSTOMERDto model)
        {
            throw new NotImplementedException();
        }

        public TB_M_CUSTOMERService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
