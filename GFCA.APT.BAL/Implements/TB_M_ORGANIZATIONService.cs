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
    public class TB_M_ORGANIZATIONService : ServiceBase, ITB_M_ORGANIZATIONService
    {
        public static TB_M_ORGANIZATIONService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new TB_M_ORGANIZATIONService(uow);
            return svc;
        }

        public IEnumerable<TB_M_ORGANIZATIONDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public TB_M_ORGANIZATIONDto GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Create(TB_M_ORGANIZATIONDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Edit(TB_M_ORGANIZATIONDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Remove(TB_M_ORGANIZATIONDto model)
        {
            throw new NotImplementedException();
        }

        public TB_M_ORGANIZATIONService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
