using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain;

namespace GFCA.APT.DAL.Implements
{
    public class SaleForecastRepository : RepositoryBase, ISaleForecastRepository
    {

        public SaleForecastRepository(IDbTransaction transaction) : base(transaction) { }

        public IEnumerable<SaleForecastHeaderDto> GetSaleForecastAll()
        {
            string sqlQuery = @"
                SELECT  DOC_SFCH_ID
                , DOC_CODE
                , DOC_VER
                , DOC_REV
                , CUST_CODE
                , (SELECT TOP 1 b.CUST_NAME FROM TB_M_CUSTOMER b WHERE b.CUST_CODE = a.CUST_CODE) CUST_NAME
                , CHANNEL_CODE
                , (SELECT TOP 1 b.CHANNEL_NAME FROM TB_M_CHANNEL b WHERE b.CHANNEL_CODE = a.CHANNEL_CODE) CHANNEL_NAME
                , STATUS as DOC_STATUS
                , COMMENT
                , FLAG_ROW
                , CREATED_BY
                , CREATED_DATE
                , UPDATED_BY
                , UPDATED_DATE
                  FROM TB_T_SALES_FORECAST_H a;";
            var query = Connection.Query<SaleForecastHeaderDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;

        }
        public SaleForecastHeaderDto GetSaleForecastByItemID(int DOC_SFCH_ID)
        {
            string sqlQuery = @"
                SELECT DOC_SFCH_ID
                , DOC_CODE
                , DOC_VER
                , DOC_REV
                , CUST_CODE
                , (SELECT TOP 1 b.CUST_NAME FROM TB_M_CUSTOMER b WHERE b.CUST_CODE = a.CUST_CODE) CUST_NAME
                , CHANNEL_CODE
                , (SELECT TOP 1 b.CHANNEL_NAME FROM TB_M_CHANNEL b WHERE b.CHANNEL_CODE = a.CHANNEL_CODE) CHANNEL_NAME
                , STATUS as DOC_STATUS
                , COMMENT
                , FLAG_ROW
                , CREATED_BY
                , CREATED_DATE
                , UPDATED_BY
                , UPDATED_DATE
                  FROM TB_T_SALES_FORECAST_H a 
                WHERE DOC_SFCH_ID = @DOC_SFCH_ID;";
            var parms = new
            {
                DOC_SFCH_ID = DOC_SFCH_ID
            };

            var query = Connection.QueryFirstOrDefault<SaleForecastHeaderDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }
        public void InsertSaleForecastHeader(SaleForecastHeaderDto entity)
        {
            string sqlCommand = @"
                INSERT INTO TB_T_SALES_FORECAST_H
                (
                  DOC_CODE
                , DOC_VER
                , DOC_REV
                , CUST_CODE
                , CHANNEL_CODE
                , STATUS
                , COMMENT
                , FLAG_ROW
                , CREATED_BY
                , CREATED_DATE
                ) VALUES (
                  @DOC_CODE
                , @DOC_VER
                , @DOC_REV
                , @CUST_CODE
                , @CHANNEL_CODE
                , @DOC_STATUS
                , NULL
                , @FLAG_ROW
                , @CREATED_BY
                , SYSDATETIME()
                ); SELECT SCOPE_IDENTITY()";

            //var test = entity.DOC_STATUS.ToValue();
            //var test2 = entity.DOC_STATUS.ToValue().ToString();
            var parms = new
            {
                DOC_CODE           = entity.DOC_CODE,
                DOC_VER            = entity.DOC_VER,
                DOC_REV            = entity.DOC_REV,
                //CLIENT_CODE        = entity.CLIENT_CODE,
                CUST_CODE          = entity.CUST_CODE,
                CHANNEL_CODE       = entity.CHANNEL_CODE,
                //COMP_CODE          = entity.COMP_CODE,
                DOC_STATUS         = entity.DOC_STATUS.ToValue(),
                //COMMENT          = entity.COMMENT,
                FLAG_ROW           = entity.FLAG_ROW,
                //REQUESTER_ORG_CODE = entity.ORG_CODE,
                CREATED_BY         = entity.CREATED_BY
            };

            int DOC_SFCH_ID = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
            entity.DOC_SFCH_ID = DOC_SFCH_ID;
        }
        public void UpdateSaleForecastHeader(SaleForecastHeaderDto entity)
        {
            string sqlCommand = @"
                UPDATE TB_T_SALES_FORECAST_H
                SET
                 DOC_CODE     = @DOC_CODE
                ,DOC_VER      = @DOC_VER
                ,DOC_REV      = @DOC_REV
                ,CUST_CODE    = @CUST_CODE
                ,CHANNEL_CODE = @CHANNEL_CODE
                ,DOC_STATUS   = @DOC_STATUS
                ,COMMENT      = @COMMENT
                ,FLAG_ROW     = @FLAG_ROW
                ,UPDATED_BY   = @UPDATED_BY
                ,UPDATED_DATE = @UPDATED_DATE
                WHERE DOC_SFCH_ID = @DOC_SFCH_ID;";

            var parms = new
            {
                DOC_SFCH_ID = entity.DOC_SFCH_ID,
                DOC_CODE = entity.DOC_CODE,
                DOC_VER = entity.DOC_VER,
                DOC_REV = entity.DOC_REV,
                //CLIENT_CODE = entity.CLIENT_CODE,
                CUST_CODE = entity.CUST_CODE,
                CHANNEL_CODE = entity.CHANNEL_CODE,
                DOC_STATUS = entity.DOC_STATUS.ToValue(),
                COMMENT = entity.COMMENT,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
                //ORG_CODE = entity.ORG_CODE,
                //COMP_CODE = entity.COMP_CODE,
                //REQUESTER = entity.REQUESTER
            };

            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }
        public void DeleteSaleForecastHeader(int DOC_SFCH_ID)
        {
            string sqlCommand = @"DELETE TB_T_SALES_FORECAST_H WHERE DOC_SFCH_ID = @DOC_SFCH_ID;";

            var parms = new
            {
                DOC_SFCH_ID = DOC_SFCH_ID
            };

            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }

        public SaleForecastDetailDto GetDetailItem(int DOC_SFCD_ID)
        {
            string sqlQuery = @"
                SELECT
                  a.DOC_SFCH_ID
                , a.DOC_SFCD_ID
                , a.PROD_CODE
                , (SELECT TOP 1 b.PROD_NAME FROM TB_M_PRODUCT b WHERE b.PROD_CODE = a.PROD_CODE) PROD_NAME
                , a.Brand as BRAND_CODE
                , (SELECT TOP 1 b.BRAND_NAME FROM TB_M_BRAND b WHERE b.BRAND_CODE = a.Brand) BRAND_NAME
                , a.SIZE
                , (SELECT TOP 1 b.SIZE_NAME FROM TB_M_SIZE b WHERE b.SIZE_CODE = a.SIZE) SIZE_NAME
                , a.UOM
                , a.PACK
                , (SELECT TOP 1 b.PACK_NAME FROM TB_M_PACK b WHERE b.PACK_CODE = a.PACK) SIZE_NAME
                , a.M1Sales
                , a.M2Sales
                , a.M3Sales
                , a.M4Sales
                , a.M5Sales
                , a.M6Sales
                , a.M7Sales
                , a.M8Sales
                , a.M9Sales
                , a.M10Sales
                , a.M11Sales
                , a.M12Sales
                , a.M1FOC
                , a.M2FOC
                , a.M3FOC
                , a.M4FOC
                , a.M5FOC
                , a.M6FOC
                , a.M7FOC
                , a.M8FOC
                , a.M9FOC
                , a.M10FOC
                , a.M11FOC
                , a.M12FOC
                , a.Year
                , a.Status as DOC_STATUS
                , a.Create_By as CREATED_BY
                , a.Create_Date as CREATED_DATE
                , a.Update_By as UPDATED_BY
                , a.Update_Date as UPDATED_DATE
                FROM TB_T_SALES_FORECAST_D a 
                WHERE
                DOC_SFCD_ID = @DOC_SFCD_ID
                ;";

            var parms = new
            {
                DOC_SFCD_ID = DOC_SFCD_ID
            };
            var query = Connection.QueryFirstOrDefault<SaleForecastDetailDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }
        public IEnumerable<SaleForecastDetailDto> GetDetailItems(int DOC_SFCH_ID)
        {
            string sqlQuery = @"
                SELECT
                  a.DOC_SFCH_ID
                , a.DOC_SFCD_ID
                , a.PROD_CODE
                , (SELECT TOP 1 b.PROD_NAME FROM TB_M_PRODUCT b WHERE b.PROD_CODE = a.PROD_CODE) PROD_NAME
                , a.Brand as BRAND_CODE
                , (SELECT TOP 1 b.BRAND_NAME FROM TB_M_BRAND b WHERE b.BRAND_CODE = a.Brand) BRAND_NAME
                , a.SIZE
                , (SELECT TOP 1 b.SIZE_NAME FROM TB_M_SIZE b WHERE b.SIZE_CODE = a.SIZE) SIZE_NAME
                , a.UOM
                , a.PACK
                , (SELECT TOP 1 b.PACK_NAME FROM TB_M_PACK b WHERE b.PACK_CODE = a.PACK) PACK_NAME
                , a.M1Sales
                , a.M2Sales
                , a.M3Sales
                , a.M4Sales
                , a.M5Sales
                , a.M6Sales
                , a.M7Sales
                , a.M8Sales
                , a.M9Sales
                , a.M10Sales
                , a.M11Sales
                , a.M12Sales
                , a.M1FOC
                , a.M2FOC
                , a.M3FOC
                , a.M4FOC
                , a.M5FOC
                , a.M6FOC
                , a.M7FOC
                , a.M8FOC
                , a.M9FOC
                , a.M10FOC
                , a.M11FOC
                , a.M12FOC
                , a.Year
                , a.Status as DOC_STATUS
                , a.Create_By as CREATED_BY
                , a.Create_Date as CREATED_DATE
                , a.Update_By as UPDATED_BY
                , a.Update_Date as UPDATED_DATE
                FROM TB_T_SALES_FORECAST_D a 
                WHERE 
                DOC_SFCH_ID = @DOC_SFCH_ID;";

            var parms = new
            {
                DOC_SFCH_ID = DOC_SFCH_ID
            };
            var query = Connection.Query<SaleForecastDetailDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                ).ToList();

            return query;
        }
        //        public IEnumerable<SaleForecastDetailDto> GetDetailItems(string docCode, int docVer = -1, int docRev = -1)
        //        {
        //            string sqlQuery = @"
        //SELECT
        //  a.DOC_SFCH_ID
        //, a.DOC_SFCD_ID
        //, a.Prd_code as PROD_CODE
        //, (SELECT TOP 1 b.PROD_NAME FROM TB_M_PRODUCT b WHERE b.PROD_CODE = a.PROD_CODE) PROD_NAME
        //, a.Brand as BRAND_CODE
        //, (SELECT TOP 1 b.BRAND_NAME FROM TB_M_BRAND b WHERE b.BRAND_CODE = a.Brand) BRAND_NAME
        //, a.SIZE
        //, (SELECT TOP 1 b.SIZE_NAME FROM TB_M_SIZE b WHERE b.SIZE_CODE = a.SIZE) SIZE_NAME
        //, a.UOM
        //, a.PACK
        //, (SELECT TOP 1 b.PACK_NAME FROM TB_M_PACK b WHERE b.PACK_CODE = a.PACK) SIZE_NAME
        //, a.M1Sales
        //, a.M2Sales
        //, a.M3Sales
        //, a.M4Sales
        //, a.M5Sales
        //, a.M6Sales
        //, a.M7Sales
        //, a.M8Sales
        //, a.M9Sales
        //, a.M10Sales
        //, a.M11Sales
        //, a.M12Sales
        //, a.M1FOC
        //, a.M2FOC
        //, a.M3FOC
        //, a.M4FOC
        //, a.M5FOC
        //, a.M6FOC
        //, a.M7FOC
        //, a.M8FOC
        //, a.M9FOC
        //, a.M10FOC
        //, a.M11FOC
        //, a.M12FOC
        //, a.Year
        //, a.Status as DOC_STATUS
        //, a.Create_By as CREATED_BY
        //, a.Create_Date as CREATED_DATE
        //, a.Update_By as UPDATED_BY
        //, a.Update_Date as UPDATED_DATE
        //FROM TB_T_SALES_FORECAST_D a 
        //WHERE
        //CONDITION_TYPE = 'PLANNING'
        //AND DOC_CODE = @DOC_CODE
        //;";

        //            var parms = new
        //            {
        //                DOC_CODE = docCode
        //            };
        //            var query = Connection.Query<SaleForecastDetailDto>(
        //                sql: sqlQuery
        //                , param: parms
        //                , transaction: Transaction
        //                ).ToList();

        //            return query;
        //        }
        public void InsertSaleForecastDetail(SaleForecastDetailDto entity)
        {
            string sqlCommand = @"
                INSERT INTO TB_T_SALES_FORECAST_D
                (
                  DOC_SFCH_ID
                , PROD_CODE
                , Brand
                , SIZE
                , UOM
                , PACK
                , M1Sales
                , M2Sales
                , M3Sales
                , M4Sales
                , M5Sales
                , M6Sales
                , M7Sales
                , M8Sales
                , M9Sales
                , M10Sales
                , M11Sales
                , M12Sales
                , M1FOC
                , M2FOC
                , M3FOC
                , M4FOC
                , M5FOC
                , M6FOC
                , M7FOC
                , M8FOC
                , M9FOC
                , M10FOC
                , M11FOC
                , M12FOC
                , TotalSales
                , TotalFOC
                , Year
                , Status
                , Create_By
                , Create_Date
                ) VALUES (
                  @DOC_SFCH_ID
                , @PROD_CODE
                , @BRAND_CODE
                , @SIZE
                , @UOM
                , @PACK
                , @M1Sales
                , @M2Sales
                , @M3Sales
                , @M4Sales
                , @M5Sales
                , @M6Sales
                , @M7Sales
                , @M8Sales
                , @M9Sales
                , @M10Sales
                , @M11Sales
                , @M12Sales
                , @M1FOC
                , @M2FOC
                , @M3FOC
                , @M4FOC
                , @M5FOC
                , @M6FOC
                , @M7FOC
                , @M8FOC
                , @M9FOC
                , @M10FOC
                , @M11FOC
                , @M12FOC
                , @TotalSales
                , @TotalFOC
                , @YEAR
                , @DOC_STATUS
                , @CREATED_BY
                , @CREATED_DATE
                ); SELECT SCOPE_IDENTITY()";

            var parms = new
            {
                DOC_SFCH_ID = entity.DOC_SFCH_ID,
                DOC_SFCD_ID = entity.DOC_SFCD_ID,
                PROD_CODE = entity.PROD_CODE,
                BRAND_CODE = entity.BRAND_CODE,
                SIZE = entity.SIZE,
                UOM = entity.UOM,
                PACK = entity.PACK,
                M1Sales = entity.M1Sales,
                M2Sales = entity.M2Sales,
                M3Sales = entity.M3Sales,
                M4Sales = entity.M4Sales,
                M5Sales = entity.M5Sales,
                M6Sales = entity.M6Sales,
                M7Sales = entity.M7Sales,
                M8Sales = entity.M8Sales,
                M9Sales = entity.M9Sales,
                M10Sales = entity.M10Sales,
                M11Sales = entity.M11Sales,
                M12Sales = entity.M12Sales,
                M1FOC = entity.M1FOC,
                M2FOC = entity.M2FOC,
                M3FOC = entity.M3FOC,
                M4FOC = entity.M4FOC,
                M5FOC = entity.M5FOC,
                M6FOC = entity.M6FOC,
                M7FOC = entity.M7FOC,
                M8FOC = entity.M8FOC,
                M9FOC = entity.M9FOC,
                M10FOC = entity.M10FOC,
                M11FOC = entity.M11FOC,
                M12FOC = entity.M12FOC,
                TotalSales = entity.TotalSales,
                TotalFOC = entity.TotalFOC,
                YEAR = entity.YEAR,
                DOC_STATUS = entity.DOC_STATUS.ToValue(),
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            int DOC_SFCD_ID = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );

            entity.DOC_SFCD_ID = DOC_SFCD_ID;
        }
        public void UpdateSaleForecastDetail(SaleForecastDetailDto entity)
        {
            string sqlCommand = @"
                UPDATE TB_T_SALES_FORECAST_D
                SET
                  PROD_CODE     = @PROD_CODE
                , Brand     = @BRAND_CODE
                , SIZE           = @SIZE
                , UOM            = @UOM
                , PACK           = @PACK
                , M1Sales            = @M1Sales
                , M2Sales            = @M2Sales
                , M3Sales            = @M3Sales
                , M4Sales            = @M4Sales
                , M5Sales            = @M5Sales
                , M6Sales            = @M6Sales
                , M7Sales            = @M7Sales
                , M8Sales            = @M8Sales
                , M9Sales            = @M9Sales
                , M10Sales            = @M10Sales
                , M11Sales            = @M11Sales
                , M12Sales            = @M12Sales
                , M1FOC            = @M1FOC
                , M2FOC            = @M2FOC
                , M3FOC            = @M3FOC
                , M4FOC            = @M4FOC
                , M5FOC            = @M5FOC
                , M6FOC            = @M6FOC
                , M7FOC            = @M7FOC
                , M8FOC            = @M8FOC
                , M9FOC            = @M9FOC
                , M10FOC            = @M10FOC
                , M11FOC            = @M11FOC
                , M12FOC            = @M12FOC
                , Year              = @YEAR
                , TotalSales        = @TotalSales
                , TotalFOC          = @TotalFOC
                , Status            = @DOC_STATUS
                , Update_By         = @UPDATED_BY
                , Update_Date       = @UPDATED_DATE
                WHERE DOC_SFCD_ID = @DOC_SFCD_ID
                ;";

            /*
             DOC_CODE       = @DOC_CODE
            AND DOC_FCH_ID = @DOC_FCH_ID

             */

            var parms = new
            {
                DOC_SFCH_ID = entity.DOC_SFCH_ID,
                DOC_SFCD_ID = entity.DOC_SFCD_ID,
                PROD_CODE = entity.PROD_CODE,
                BRAND_CODE = entity.BRAND_CODE,
                SIZE = entity.SIZE,
                UOM = entity.UOM,
                PACK = entity.PACK,
                M1Sales = entity.M1Sales,
                M2Sales = entity.M2Sales,
                M3Sales = entity.M3Sales,
                M4Sales = entity.M4Sales,
                M5Sales = entity.M5Sales,
                M6Sales = entity.M6Sales,
                M7Sales = entity.M7Sales,
                M8Sales = entity.M8Sales,
                M9Sales = entity.M9Sales,
                M10Sales = entity.M10Sales,
                M11Sales = entity.M11Sales,
                M12Sales = entity.M12Sales,
                M1FOC = entity.M1FOC,
                M2FOC = entity.M2FOC,
                M3FOC = entity.M3FOC,
                M4FOC = entity.M4FOC,
                M5FOC = entity.M5FOC,
                M6FOC = entity.M6FOC,
                M7FOC = entity.M7FOC,
                M8FOC = entity.M8FOC,
                M9FOC = entity.M9FOC,
                M10FOC = entity.M10FOC,
                M11FOC = entity.M11FOC,
                M12FOC = entity.M12FOC,
                TotalSales = entity.TotalSales,
                TotalFOC = entity.TotalFOC,
                YEAR = entity.YEAR,
                DOC_STATUS = entity.DOC_STATUS.ToValue(),
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE?.ToDateTime2(),
            };

            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );

        }
        public void DeleteSaleForecastDetail(int DOC_SFCD_ID)
        {
            string sqlCommand = @"DELETE TB_T_SALES_FORECAST_D WHERE DOC_SFCD_ID = @DOC_SFCD_ID;";

            var parms = new
            {
                DOC_SFCD_ID = DOC_SFCD_ID
            };

            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }

        public IEnumerable<SaleForecastDetailDto> GetDetailItemToExport(int DOC_SFCH_ID)
        {
            string sqlQuery = @"
                SELECT
                  isnull(a.DOC_SFCH_ID, @DOC_SFCH_ID) as DOC_SFCH_ID
                , a.DOC_SFCD_ID
                , t.PROD_CODE
                , t.PROD_NAME
                , t.MAT_GROUP4 as BRAND_CODE
                , t.MAT_GROUP4_DESC as BRAND_NAME
                , t.SIZE
                , (SELECT TOP 1 b.SIZE_NAME FROM TB_M_SIZE b WHERE b.SIZE_CODE = t.SIZE) SIZE_NAME
                , t.UOM_SIZE as UOM
                , t.PACK
                , t.PACK_DESC as PACK_NAME
                , a.M1Sales
                , a.M2Sales
                , a.M3Sales
                , a.M4Sales
                , a.M5Sales
                , a.M6Sales
                , a.M7Sales
                , a.M8Sales
                , a.M9Sales
                , a.M10Sales
                , a.M11Sales
                , a.M12Sales
                , a.M1FOC
                , a.M2FOC
                , a.M3FOC
                , a.M4FOC
                , a.M5FOC
                , a.M6FOC
                , a.M7FOC
                , a.M8FOC
                , a.M9FOC
                , a.M10FOC
                , a.M11FOC
                , a.M12FOC
                , a.Year
                , a.Status as DOC_STATUS
                , a.Create_By as CREATED_BY
                , a.Create_Date as CREATED_DATE
                , a.Update_By as UPDATED_BY
                , a.Update_Date as UPDATED_DATE 
                FROM TB_M_PRODUCT t 
				left join (SELECT * FROM TB_T_SALES_FORECAST_D WHERE DOC_SFCH_ID = @DOC_SFCH_ID) a ON t.PROD_CODE = a.PROD_CODE 
				WHERE 
				t.FLAG_ROW = 'S'";

            var parms = new
            {
                DOC_SFCH_ID = DOC_SFCH_ID
            };
            var query = Connection.Query<SaleForecastDetailDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                ).ToList();

            return query;
        }


    }
}
