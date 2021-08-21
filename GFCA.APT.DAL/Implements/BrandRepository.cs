using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class BrandRepository : RepositoryBase, IBrandRepository
    {
        
        public BrandRepository(IDbTransaction transaction): base(transaction) { }

        public BrandDto GetById(int id)
        {
            string sqlQuery = @"SELECT b.CLIENT_CODE, a.* 
FROM TB_M_BRAND a
LEFT JOIN TB_M_CLIENT b on (b.CLIENT_ID = b.CLIENT_ID)
WHERE BRAND_ID = @BRAND_ID;";
            var query = Connection.Query<BrandDto>(
                sql: sqlQuery,
                param: new { BRAND_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public BrandDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_BRAND a
LEFT JOIN TB_M_CLIENT b on (b.CLIENT_ID = b.CLIENT_ID)
WHERE a.BRAND_CODE = @BRAND_CODE;";
            var query = Connection.Query<BrandDto>(
                sql: sqlQuery,
                param: new { BRAND_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<BrandDto> All()
        {
            string sqlQuery = @"SELECT b.CLIENT_CODE, a.* 
FROM TB_M_BRAND a
LEFT JOIN TB_M_CLIENT b on (b.CLIENT_ID = b.CLIENT_ID);";
            var query = Connection.Query<BrandDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(BrandDto entity)
        {
            string sqlExecute =
@"INSERT INTO TB_M_BRAND
(
  BRAND_CODE
, CLIENT_ID
, BRAND_NAME
, BRAND_DESC
, FLAG_ROW
, CREATED_BY
, CREATED_DATE
) VALUES (
  @BRAND_CODE
, @CLIENT_ID
, @BRAND_NAME
, @BRAND_DESC
, @FLAG_ROW
, @CREATED_BY
, @CREATED_DATE
); SELECT SCOPE_IDENTITY()
";

            var parms = new
            {
                //BRAND_ID = 0,
                BRAND_CODE = entity.BRAND_CODE,
                CLIENT_ID = entity.CLIENT_ID,
                BRAND_NAME = entity.BRAND_NAME,
                BRAND_DESC = entity.BRAND_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
                //UPDATED_BY = entity.UPDATED_BY,
                //UPDATED_DATE = entity.UPDATED_DATE
            };

            entity.BRAND_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(BrandDto entity)
        {
            string sqlExecute =
@"UPDATE TB_M_BRAND
SET
  BRAND_CODE   = @BRAND_CODE
, CLIENT_ID    = @CLIENT_ID
, BRAND_NAME   = @BRAND_NAME
, BRAND_DESC   = @BRAND_DESC
, FLAG_ROW     = @FLAG_ROW
, UPDATED_BY   = @UPDATED_BY
, UPDATED_DATE = @UPDATED_DATE
WHERE
BRAND_ID = @BRAND_ID;
";

            var parms = new
            {
                BRAND_ID = entity.BRAND_ID,
                BRAND_CODE = entity.BRAND_CODE,
                CLIENT_ID = entity.CLIENT_ID,
                BRAND_NAME = entity.BRAND_NAME,
                BRAND_DESC = entity.BRAND_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                //CREATED_BY = entity.CREATED_BY,
                //CREATED_DATE = entity.CREATED_DATE,
                UPDATED_BY = entity.UPDATED_BY,
                //UPDATED_DATE = entity.UPDATED_DATE
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

            string sqlExecute =
@"DELETE TB_M_BRAND
WHERE
BRAND_ID = @BRAND_ID;
";
            var parms = new
            {
                BRAND_ID = id,
                //BRAND_CODE = entity.BRAND_CODE,
                //BRAND_NAME = entity.BRAND_NAME,
                //BRAND_DESC = entity.BRAND_DESC,
                //FLAG_ROW = entity.FLAG_ROW,
                //CREATED_BY = entity.CREATED_BY,
                //CREATED_DATE = entity.CREATED_DATE,
                //UPDATED_BY = entity.UPDATED_BY,
                //UPDATED_DATE = entity.UPDATED_DATE
                //UPDATED_DATE = DateTime.UtcNow
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
