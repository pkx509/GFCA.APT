using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;

namespace GFCA.APT.DAL.Implements
{
    public class FixedContractRepository : RepositoryBase, IFixedContractRepository
    {

        public FixedContractRepository(IDbTransaction transaction) : base(transaction) { }

        public IEnumerable<FixedContractDto> All()
        {
            string sqlQuery = @"SELECT FCH.DOC_CODE,
                                (SELECT TOP 1 CHANNEL_NAME  from [dbo].[TB_M_CHANNEL] where CHANNEL_CODE = FCH.CHANNEL_CODE) as CHANNEL_NAME,
                                FCH.CREATED_BY AS REQUESTER,
                                (SELECT TOP 1 CLIENT_NAME  from [dbo].[TB_M_CLIENT] where CLIENT_CODE = FCH.CLIENT_CODE) as CLIENT_NAME,
                                FCH.CREATED_DATE
                                FROM TB_T_FIXED_CONTRACT_H FCH";

            var query = Connection.Query<FixedContractDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string code)
        {
            throw new NotImplementedException();
        }

        public FixedContractDto GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public void Insert(FixedContractDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(FixedContractDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
