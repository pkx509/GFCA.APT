using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;


namespace GFCA.APT.DAL.Implements
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {

        public ProductRepository(IDbTransaction transaction) : base(transaction) { }


        public ProductDto GetById(int id)
        {
            string sqlQuery = "SELECT * FROM TB_M_PRODUCT WHERE PROD_ID = @PROD_ID;";
            var query = Connection.Query<ProductDto>(
                sql: sqlQuery,
                param: new { PROD_ID = id },
                transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public ProductDto GetByCode(string code)
        {
            string sqlQuery = "SELECT * FROM TB_M_PRODUCT WHERE PROD_CODE = @PROD_CODE;";
            var query = Connection.Query<ProductDto>(
                sql: sqlQuery,
                param: new { PROD_CODE = code },
                transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<ProductDto> All()
        {
            string sqlQuery = @"SELECT P.*,
                                          (SELECT TOP 1 C.CUST_CODE + '-' + C.CUST_NAME  FROM TB_M_CUSTOMER as C WHERE P.CUST_CODE = C.CUST_CODE) as CUST_NAME,
		                                  (SELECT TOP 1 C.EMIS_CODE + '-' + C.EMIS_NAME  FROM TB_M_EMISSION as C WHERE P.EMIS_CODE = C.EMIS_CODE) as EMIS_NAME
                             FROM TB_M_PRODUCT  as P";

            var query = Connection.Query<ProductDto>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(ProductDto entity)
        {
            string sqlExecute = "INSERT INTO TB_M_PRODUCT(PROD_CODE,PROD_NAME,CUST_CODE,MAT_CODE,ORG_CODE,DIV_CODE,EMIS_CODE,MAT_GROUP,MAT_GROUP_DESC,MAT_GROUP1,MAT_GROUP1_DESC,MAT_GROUP2,MAT_GROUP2_DESC,MAT_GROUP3,MAT_GROUP3_DESC,FORMULA,PACK,PACK_DESC,UNIT_CODE,FLAG_ROW,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE) VALUES (@PROD_CODE,@PROD_NAME,@CUST_CODE,@MAT_CODE,@ORG_CODE,@DIV_CODE,@EMIS_CODE,@MAT_GROUP,@MAT_GROUP_DESC,@MAT_GROUP1,@MAT_GROUP1_DESC,@MAT_GROUP2,@MAT_GROUP2_DESC,@MAT_GROUP3,@MAT_GROUP3_DESC,@FORMULA,@PACK,@PACK_DESC,@UNIT_CODE,@FLAG_ROW,@CREATED_BY,@CREATED_DATE,@UPDATED_BY,@UPDATED_DATE);";
            var parameters = new
            {
               
                PROD_CODE = entity.PROD_CODE,
                PROD_NAME = entity.PROD_NAME,
                CUST_CODE = entity.CUST_CODE,
                MAT_CODE = entity.MAT_CODE,
                ORG_CODE = entity.ORG_CODE,
                DIV_CODE = entity.DIV_CODE,
                EMIS_CODE = entity.EMIS_CODE,
                MAT_GROUP = entity.MAT_GROUP,
                MAT_GROUP_DESC = entity.MAT_GROUP_DESC,
                MAT_GROUP1 = entity.MAT_GROUP1,
                MAT_GROUP1_DESC = entity.MAT_GROUP1_DESC,
                MAT_GROUP2 = entity.MAT_GROUP2,
                MAT_GROUP2_DESC = entity.MAT_GROUP2_DESC,
                MAT_GROUP3 = entity.MAT_GROUP3,
                MAT_GROUP3_DESC = entity.MAT_GROUP3_DESC,
                FORMULA = entity.FORMULA,
                PACK = entity.PACK,
                PACK_DESC = entity.PACK_DESC,
                // UNIT_CODE = entity.UNIT_CODE,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE,
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parameters,
                transaction: Transaction
            );

        }
        public void Update(ProductDto entity)
        {
            string sqlExecute =
@"UPDATE TB_M_PRODUCT
SET  
  CUST_CODE   = @CUST_CODE
, EMIS_CODE   = @EMIS_CODE
, UPDATED_BY   = @UPDATED_BY
, UPDATED_DATE = @UPDATED_DATE
WHERE
PROD_CODE = @PROD_CODE;
";

            var parms = new
            {
                PROD_CODE= entity.PROD_CODE,
                CUST_CODE = entity.CUST_CODE,
                FLAG_ROW = entity.FLAG_ROW,
                EMIS_CODE = entity.EMIS_CODE,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE?.ToDateTime2()
            };

            Connection.ExecuteScalar<string>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

        public void Delete(string code)
        {

            string sqlExecute =
@"DELETE TB_M_PRODUCT
WHERE
PROD_CODE = @PROD_CODE;
";
            var parms = new{ PROD_CODE = code };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

        /*
        public IEnumerable<APTProduct> GetMatGroup()
        {
            string sqlQuery = "SELECT DISTINCT   MatGroup,MatGroupDesc FROM APTProduct;";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public IEnumerable<APTProduct> GetMatGroup1()
        {
            string sqlQuery = "SELECT DISTINCT   MatGroup1,MatGroup1 FROM APTProduct;";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public IEnumerable<APTProduct> GetMatGroup2()
        {
            string sqlQuery = "SELECT DISTINCT   MatGroup2,MatGroup2 FROM APTProduct;";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public IEnumerable<APTProduct> GetMatGroup3()
        {
            string sqlQuery = "SELECT DISTINCT   MatGroup3,MatGroup3 FROM APTProduct;";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }
        public IEnumerable<APTProduct> GetMatGroup4()
        {
            string sqlQuery = "SELECT DISTINCT   MatGroup4,MatGroup4 FROM APTProduct;";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        IEnumerable<APTProduct> IProductRepository.GetFormula()
        {
            string sqlQuery = "SELECT DISTINCT   Formula FROM APTProduct;";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        IEnumerable<APTProduct> IProductRepository.GetPack()
        {
            string sqlQuery = "SELECT DISTINCT   PACK,PackDetail FROM APTProduct";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public IEnumerable<APTProduct> GetSize()
        {
            string sqlQuery = "SELECT DISTINCT   Size FROM APTProduct";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public IEnumerable<APTProduct> GetSixeUOM()
        {
            string sqlQuery = "SELECT DISTINCT   SizeUOM FROM APTProduct";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public IEnumerable<APTProduct> GetCONV_FCL()
        {
            string sqlQuery = "SELECT DISTINCT   CONV_FCL FROM APTProduct";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public IEnumerable<APTProduct> GetCONV_L()
        {
            string sqlQuery = "SELECT DISTINCT   CONV_L FROM APTProduct";
            var query = Connection.Query<APTProduct>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }
        */
        
    }
}
