using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class UnitRepository : RepositoryBase, IUnitRepository
    {

        public UnitRepository(IDbTransaction transaction): base(transaction) { }

        public UnitDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM TB_M_UNIT WHERE UNIT_ID = @UNIT_ID;";
            var query = Connection.Query<UnitDto>(
                sql: sqlQuery,
                param: new { UNIT_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public UnitDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_UNIT WHERE UNIT_CODE = @UNIT_CODE;";
            var query = Connection.Query<UnitDto>(
                sql: sqlQuery,
                param: new { UNIT_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();
       
            return query;
        }
        public IEnumerable<UnitDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_UNIT";
            var query = Connection.Query<UnitDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(UnitDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_UNIT
                                (
                                  PARENT_ID
                                , UNIT_CODE
                                , UNIT_NAME
                                , UNIT_TYPE
                                , FACTOR
                                , UNIT_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @PARENT_ID   
                                , @UNIT_CODE
                                , @UNIT_NAME   
                                , @UNIT_TYPE
                                , @FACTOR
                                , @UNIT_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                PARENT_ID = entity.PARENT_ID,
                UNIT_CODE = entity.UNIT_CODE,
                UNIT_NAME = entity.UNIT_NAME,
                UNIT_TYPE = entity.UNIT_TYPE,
                FACTOR = entity.FACTOR,
                UNIT_DESC = entity.UNIT_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.UNIT_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(UnitDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_UNIT
                                SET
                                  PARENT_ID     = @PARENT_ID
                                , UNIT_CODE     = @UNIT_CODE
                                , UNIT_NAME     = @UNIT_NAME
                                , UNIT_TYPE     = @UNIT_TYPE
                                , FACTOR        = @FACTOR
                                , UNIT_DESC     = @UNIT_DESC
                                , FLAG_ROW      = @FLAG_ROW
                                , UPDATED_BY    = @UPDATED_BY
                                , UPDATED_DATE  = @UPDATED_DATE
                                WHERE
                                UNIT_ID = @UNIT_ID;
                                ";

            var parms = new
        {
                UNIT_ID = entity.UNIT_ID,
                PARENT_ID = entity.PARENT_ID,
                UNIT_CODE = entity.UNIT_CODE,
                UNIT_NAME = entity.UNIT_NAME,
                UNIT_TYPE = entity.UNIT_TYPE,
                FACTOR = entity.FACTOR,
                UNIT_DESC = entity.UNIT_DESC,
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
            string sqlExecute = @"DELETE TB_M_UNIT WHERE UNIT_ID = @UNIT_ID;";
            var parms = new { UNIT_ID = id };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
