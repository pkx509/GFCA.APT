using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class DistributorRepository : RepositoryBase, IDistributorRepository
    {

        public DistributorRepository(IDbTransaction transaction): base(transaction) { }

        public DistributorDto GetById(int id)
        {
            string sqlQuery = @"SELECT a.* 
                                , (SELECT TOP 1 b.EMIS_CODE FROM TB_M_EMISSION b WHERE b.EMIS_ID = a.EMIS_ID) EMIS_CODE
                                FROM TB_M_DISTRIBUTOR a
                                WHERE DISTB_ID = @DISTB_ID;";
            var query = Connection.Query<DistributorDto>(
                sql: sqlQuery,
                param: new { DISTB_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public DistributorDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT a.* 
                                , (SELECT TOP 1 b.EMIS_CODE FROM TB_M_EMISSION b WHERE b.EMIS_ID = a.EMIS_ID) EMIS_CODE
                                FROM TB_M_DISTRIBUTOR a 
                                WHERE DISTB_CODE = @DISTB_CODE;";
            var query = Connection.Query<DistributorDto>(
                sql: sqlQuery,
                param: new { DISTB_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();
       
            return query;
        }
        public IEnumerable<DistributorDto> All()
        {
            string sqlQuery = @"SELECT a.* 
                                , (SELECT TOP 1 b.EMIS_CODE FROM TB_M_EMISSION b WHERE b.EMIS_ID = a.EMIS_ID) EMIS_CODE
                                FROM TB_M_DISTRIBUTOR a;";
            var query = Connection.Query<DistributorDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(DistributorDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_DISTRIBUTOR
                                (
                                  EMIS_ID
                                , DISTB_CODE
                                , DISTB_NAME
                                , DISTB_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @EMIS_ID
                                , @DISTB_CODE
                                , @DISTB_NAME
                                , @DISTB_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                EMIS_ID = entity.EMIS_ID,
                DISTB_CODE = entity.DISTB_CODE,
                DISTB_NAME = entity.DISTB_NAME,
                DISTB_DESC = entity.DISTB_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.DISTB_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(DistributorDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_DISTRIBUTOR
                                SET
                                  EMIS_ID = @EMIS_ID
                                  DISTB_CODE   = @DISTB_CODE
                                , DISTB_NAME   = @DISTB_NAME
                                , DISTB_DESC   = @DISTB_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                DISTB_ID = @DISTB_ID;
                                ";

            var parms = new
        {
                DISTB_ID = entity.DISTB_ID,
                EMIS_ID = entity.EMIS_ID,
                DISTB_CODE = entity.DISTB_CODE,
                DISTB_NAME = entity.DISTB_NAME,
                DISTB_DESC = entity.DISTB_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE?.ToDateTime2()
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

        public void Delete(int id)
        {
            string sqlExecute = @"DELETE TB_M_DISTRIBUTOR WHERE DISTB_ID = @DISTB_ID;";
            var parms = new { DISTB_ID = id };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
