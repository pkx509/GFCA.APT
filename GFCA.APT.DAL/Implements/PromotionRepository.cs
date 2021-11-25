using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GFCA.APT.DAL.Implements
{
    public class PromotionRepository : RepositoryBase, IPromotionRepository
    {
        public PromotionRepository(IDbTransaction transaction) : base(transaction) { }

        public IEnumerable<PromotionPlanngOverviewDto> GetPromotionPlanAll()
        {
            string sqlQuery =
@"SELECT
  A.DOC_PROM_PH_ID
, A.DOC_CODE
, A.DOC_VER
, A.DOC_REV
, A.DOC_STATUS
, A.PROMO_NAME
, A.PROMO_START
, A.PROMO_END
, A.BUYING_START
, A.BUYING_END
, A.CLIENT_CODE
, (SELECT TOP 1 CLIENT_NAME FROM TB_M_CLIENT WHERE CLIENT_CODE = A.CLIENT_CODE) 'CLIENT_NAME'
, A.CHANNEL_CODE
, (SELECT TOP 1 CHANNEL_NAME FROM TB_M_CHANNEL WHERE CHANNEL_CODE = A.CHANNEL_CODE) 'CHANNEL_NAME'
, A.CUST_CODE
, (SELECT TOP 1 CUST_NAME FROM TB_M_CUSTOMER WHERE CUST_NAME = A.CUST_CODE) 'CUST_NAME'
, A.CUST_SEGMENT
, A.TOTAL_EST_SALE
, A.TOTAL_EST_INVEST
, A.SALE_VS_INVEST
, A.COMMENT
, A.FLAG_ROW
, A.CREATED_BY
, A.CREATED_DATE
, A.UPDATED_BY
, A.UPDATED_DATE
FROM TB_T_PROMOTION_H A";

            var query = Connection.Query<PromotionPlanngOverviewDto>(
                sql: sqlQuery
                , transaction: Transaction
                );

            return query;
        }
        public PromotionPlanngOverviewDto GetPromotionPlanByItemID(int DOC_PROM_PH_ID)
        {
            string sqlQuery =
@"SELECT
  A.DOC_PROM_PH_ID
, A.DOC_CODE
, A.DOC_VER
, A.DOC_REV
, A.DOC_STATUS
, A.PROMO_NAME
, A.PROMO_START
, A.PROMO_END
, A.BUYING_START
, A.BUYING_END
, A.CLIENT_CODE
, (SELECT TOP 1 CLIENT_NAME FROM TB_M_CLIENT WHERE CLIENT_CODE = A.CLIENT_CODE) 'CLIENT_NAME'
, A.CHANNEL_CODE
, (SELECT TOP 1 CHANNEL_NAME FROM TB_M_CHANNEL WHERE CHANNEL_CODE = A.CHANNEL_CODE) 'CHANNEL_NAME'
, A.CUST_CODE
, (SELECT TOP 1 CUST_NAME FROM TB_M_CUSTOMER WHERE CUST_NAME = A.CUST_CODE) 'CUST_NAME'
, A.CUST_SEGMENT
, A.TOTAL_EST_SALE
, A.TOTAL_EST_INVEST
, A.SALE_VS_INVEST
, A.COMMENT
, A.FLAG_ROW
, A.CREATED_BY
, A.CREATED_DATE
, A.UPDATED_BY
, A.UPDATED_DATE
FROM TB_T_PROMOTION_H A
WHERE A.DOC_PROM_PH_ID = @DOC_PROM_PH_ID;";

            var parms = new
            {
                DOC_PROM_PH_ID = DOC_PROM_PH_ID
            };

            var query = Connection.QueryFirstOrDefault<PromotionPlanngOverviewDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }
        public void InsertOverview(PromotionPlanngOverviewDto entity)
        {
            string sqlComamnd =
@"INSERT INTO TB_T_PROMOTION_H
(
  DOC_CODE
, DOC_VER
, DOC_REV
, DOC_STATUS
, PROMO_NAME
, PROMO_START
, PROMO_END
, BUYING_START
, BUYING_END
, CLIENT_CODE
, CHANNEL_CODE
, CUST_CODE
, CUST_SEGMENT
, TOTAL_EST_SALE
, TOTAL_EST_INVEST
, SALE_VS_INVEST
, COMMENT
, FLAG_ROW
, CREATED_BY
, CREATED_DATE
) VALUES (
  @DOC_CODE
, @DOC_VER
, @DOC_REV
, @DOC_STATUS
, @PROMO_NAME
, @PROMO_START
, @PROMO_END
, @BUYING_START
, @BUYING_END
, @CLIENT_CODE
, @CHANNEL_CODE
, @CUST_CODE
, @CUST_SEGMENT
, @TOTAL_EST_SALE
, @TOTAL_EST_INVEST
, @SALE_VS_INVEST
, NULL
, @FLAG_ROW
, @CREATED_BY
, SYSDATETIME()
); SELECT SCOPE_IDENTITY()";
            var parms = new
            {
                DOC_CODE         = entity.DOC_CODE,
                DOC_VER          = entity.DOC_VER,
                DOC_REV          = entity.DOC_REV,
                DOC_STATUS       = entity.DOC_STATUS.ToString(),
                PROMO_NAME       = entity.PROMO_NAME,
                PROMO_START      = entity.PROMO_START,
                PROMO_END        = entity.PROMO_END,
                BUYING_START     = entity.BUYING_START,
                BUYING_END       = entity.BUYING_END,
                CLIENT_CODE      = entity.CLIENT_CODE,
                CHANNEL_CODE     = entity.CHANNEL_CODE,
                CUST_CODE        = entity.CUST_CODE,
                CUST_SEGMENT     = entity.CUST_SEGMENT,
                TOTAL_EST_SALE   = entity.TOTAL_EST_SALE,
                TOTAL_EST_INVEST = entity.TOTAL_EST_INVEST,
                SALE_VS_INVEST   = entity.SALE_VS_INVEST,
                //COMMENT        = entity.COMMENT,
                FLAG_ROW         = entity.FLAG_ROW.ToString(),
                CREATED_BY       = entity.CREATED_BY
            };

            var DOC_PROM_PH_ID = Connection.ExecuteScalar<int>(
                sql: sqlComamnd,
                param: parms,
                transaction: Transaction
            );

            entity.DOC_PROM_PH_ID = DOC_PROM_PH_ID;
        }
        public void UpdateOverview(PromotionPlanngOverviewDto entity)
        {
            string sqlComamnd =
@"UPDATE TB_T_PROMOTION_H
SET
  DOC_CODE         = @DOC_CODE
, DOC_VER          = @DOC_VER
, DOC_REV          = @DOC_REV
, DOC_STATUS       = @DOC_STATUS
, PROMO_NAME       = @PROMO_NAME
, PROMO_START      = @PROMO_START
, PROMO_END        = @PROMO_END
, BUYING_START     = @BUYING_START
, BUYING_END       = @BUYING_END
, CLIENT_CODE      = @CLIENT_CODE
, CHANNEL_CODE     = @CHANNEL_CODE
, CUST_CODE        = @CUST_CODE
, CUST_SEGMENT     = @CUST_SEGMENT
, TOTAL_EST_SALE   = @TOTAL_EST_SALE
, TOTAL_EST_INVEST = @TOTAL_EST_INVEST
, SALE_VS_INVEST   = @SALE_VS_INVEST
, COMMENT          = @COMMENT
, FLAG_ROW         = @FLAG_ROW
, UPDATED_BY       = @UPDATED_BY
, UPDATED_DATE     = SYSDATETIME()
WHERE DOC_PROM_PH_ID = @DOC_PROM_PH_ID";
            var parms = new
            {
                DOC_PROM_PH_ID   = entity.DOC_PROM_PH_ID,
                DOC_CODE         = entity.DOC_CODE,
                DOC_VER          = entity.DOC_VER,
                DOC_REV          = entity.DOC_REV,
                DOC_STATUS       = entity.DOC_STATUS.ToString(),
                PROMO_NAME       = entity.PROMO_NAME,
                PROMO_START      = entity.PROMO_START,
                PROMO_END        = entity.PROMO_END,
                BUYING_START     = entity.BUYING_START,
                BUYING_END       = entity.BUYING_END,
                CLIENT_CODE      = entity.CLIENT_CODE,
                CHANNEL_CODE     = entity.CHANNEL_CODE,
                CUST_CODE        = entity.CUST_CODE,
                CUST_SEGMENT     = entity.CUST_SEGMENT,
                TOTAL_EST_SALE   = entity.TOTAL_EST_SALE,
                TOTAL_EST_INVEST = entity.TOTAL_EST_INVEST,
                SALE_VS_INVEST   = entity.SALE_VS_INVEST,
                COMMENT          = entity.COMMENT,
                FLAG_ROW         = entity.FLAG_ROW.ToString(),
                UPDATED_BY       = entity.UPDATED_BY
            };

            var effected = Connection.ExecuteScalar<int>(
                sql: sqlComamnd,
                param: parms,
                transaction: Transaction
            );
        }
        public void DeleteOverview(int DOC_PROM_PH_ID)
        {
            string sqlComamnd = @"DELETE TB_T_PROMOTION_H WHERE DOC_PROM_PH_ID = @DOC_PROM_PH_ID;";

            var parms = new
            {
                DOC_PROM_PH_ID = DOC_PROM_PH_ID
            };
            var effected = Connection.ExecuteScalar<int>(
                sql: sqlComamnd,
                param: parms,
                transaction: Transaction
            );
        }

        public IEnumerable<PromotionPlanngInvestmentDto> GetInvestmentByHeaderID(int DOC_PROM_PH_ID)
        {
            string sqlQuery =
@"SELECT
  A.DOC_PROM_PI_ID
, A.DOC_PROM_PS_ID
, A.DOC_PROM_PH_ID
, A.DOC_CODE
, A.DOC_VER
, A.DOC_REV
, A.COMP_CODE
, (SELECT TOP 1 COMP_NAME FROM TB_M_COMPANY WHERE COMP_CODE = A.COMP_CODE) 'COMP_NAME'
, A.BRAND_CODE
, (SELECT TOP 1 BRAND_NAME FROM TB_M_BRAND WHERE BRAND_CODE = A.BRAND_CODE) 'BRAND_NAME'
, A.PROD_CODE
, (SELECT TOP 1 PROD_NAME FROM TB_M_PRODUCT WHERE PROD_CODE = A.PROD_CODE) 'PROD_NAME'
, (SELECT TOP 1 PROD_SKU FROM TB_M_PRODUCT WHERE PROD_CODE = A.PROD_CODE) 'PROD_SKU'
, A.ACTIVITY_CODE
, (SELECT TOP 1 ACTIVTITY_NAME FROM TB_M_ACTIVITY WHERE ACTIVITY_CODE = A.ACTIVITY_CODE) 'ACTIVTITY_NAME'
, A.INVEST_TYPE
, A.INVEST_VALUE
, A.INVEST_AMOUNT
, A.ACTIVITY_CODE_OTHER
, (SELECT TOP 1 ACTIVTITY_NAME FROM TB_M_ACTIVITY WHERE ACTIVITY_CODE = A.ACTIVITY_CODE_OTHER) 'ACTIVITY_NAME_OTHER'
, A.ACTIVITY_COMBINED_OTHER
, A.INCREMENT_SALE_INVEST
, A.INVEST_ACC_CODE
, (SELECT TOP 1 ACC_NAME FROM TB_M_GL_ACCOUNT WHERE ACC_CODE = A.INVEST_ACC_CODE) 'INVEST_ACC_NAME'
, A.FUND1_CODE
, A.FUND1_CENTER_CODE
, (SELECT TOP 1 CENTER_NAME FROM TB_M_COST_CENTER WHERE CENTER_CODE = A.FUND1_CENTER_CODE) 'FUND1_CENTER_NAME'
, A.FUND1_AMOUNT
, A.FUND2_CODE
, A.FUND2_CENTER_CODE
, (SELECT TOP 1 CENTER_NAME FROM TB_M_COST_CENTER WHERE CENTER_CODE = A.FUND2_CENTER_CODE) 'FUND2_CENTER_NAME'
, A.FUND2_AMOUNT
, A.REMARKS
, A.FLAG_ROW
, A.CREATED_BY
, A.CREATED_DATE
, A.UPDATED_BY
, A.UPDATED_DATE
FROM TB_T_PROMOTION_INVEST A
WHERE A.DOC_PROM_PH_ID = @DOC_PROM_PH_ID;";

            var parms = new
            {
                DOC_PROM_PH_ID = DOC_PROM_PH_ID
            };

            var query = Connection.Query<PromotionPlanngInvestmentDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;

        }
        public PromotionPlanngInvestmentDto GetInvestmentByItemID(int DOC_PROM_PI_ID)
        {
            string sqlQuery =
@"SELECT
  A.DOC_PROM_PI_ID
, A.DOC_PROM_PS_ID
, A.DOC_PROM_PH_ID
, A.DOC_CODE
, A.DOC_VER
, A.DOC_REV
, A.COMP_CODE
, (SELECT TOP 1 COMP_NAME FROM TB_M_COMPANY WHERE COMP_CODE = A.COMP_CODE) 'COMP_NAME'
, A.BRAND_CODE
, (SELECT TOP 1 BRAND_NAME FROM TB_M_BRAND WHERE BRAND_CODE = A.BRAND_CODE) 'BRAND_NAME'
, A.PROD_CODE
, (SELECT TOP 1 PROD_NAME FROM TB_M_PRODUCT WHERE PROD_CODE = A.PROD_CODE) 'PROD_NAME'
, (SELECT TOP 1 PROD_SKU FROM TB_M_PRODUCT WHERE PROD_CODE = A.PROD_CODE) 'PROD_SKU'
, A.ACTIVITY_CODE
, (SELECT TOP 1 ACTIVTITY_NAME FROM TB_M_ACTIVITY WHERE ACTIVITY_CODE = A.ACTIVITY_CODE) 'ACTIVTITY_NAME'
, A.INVEST_TYPE
, A.INVEST_VALUE
, A.INVEST_AMOUNT
, A.ACTIVITY_CODE_OTHER
, (SELECT TOP 1 ACTIVTITY_NAME FROM TB_M_ACTIVITY WHERE ACTIVITY_CODE = A.ACTIVITY_CODE_OTHER) 'ACTIVITY_NAME_OTHER'
, A.ACTIVITY_COMBINED_OTHER
, A.INCREMENT_SALE_INVEST
, A.INVEST_ACC_CODE
, (SELECT TOP 1 ACC_NAME FROM TB_M_GL_ACCOUNT WHERE ACC_CODE = A.INVEST_ACC_CODE) 'INVEST_ACC_NAME'
, A.FUND1_CODE
, A.FUND1_CENTER_CODE
, (SELECT TOP 1 CENTER_NAME FROM TB_M_COST_CENTER WHERE CENTER_CODE = A.FUND1_CENTER_CODE) 'FUND1_CENTER_NAME'
, A.FUND1_AMOUNT
, A.FUND2_CODE
, A.FUND2_CENTER_CODE
, (SELECT TOP 1 CENTER_NAME FROM TB_M_COST_CENTER WHERE CENTER_CODE = A.FUND2_CENTER_CODE) 'FUND2_CENTER_NAME'
, A.FUND2_AMOUNT
, A.REMARKS
, A.FLAG_ROW
, A.CREATED_BY
, A.CREATED_DATE
, A.UPDATED_BY
, A.UPDATED_DATE
FROM TB_T_PROMOTION_INVEST A
WHERE A.DOC_PROM_PI_ID = @DOC_PROM_PI_ID;";

            var parms = new
            {
                DOC_PROM_PI_ID = DOC_PROM_PI_ID
            };
            /*
            var maps = (delegate (PromotionPlanngInvestmentDto dto)
            {
                var data = new PromotionPlanngInvestmentDto();
                return data;
            });
            */
            var query = Connection.QueryFirstOrDefault<PromotionPlanngInvestmentDto>(
                sql: sqlQuery
                , param: parms
                //, map: maps
                //, splitOn: "DOC_PROM_PI_ID"
                , transaction: Transaction
                );

            return query;
        }
        public void InsertInvestment(PromotionPlanngInvestmentDto entity)
        {
            string sqlCommand =
@"INSERT INTO TB_T_PROMOTION_INVEST
(
  DOC_PROM_PS_ID
, DOC_PROM_PH_ID
, DOC_CODE
, DOC_VER
, DOC_REV
, COMP_CODE
, BRAND_CODE
, PROD_CODE
, PROD_SKU
, ACTIVITY_CODE
, INVEST_TYPE
, INVEST_VALUE
, INVEST_AMOUNT
, ACTIVITY_CODE_OTHER
, ACTIVITY_COMBINED_OTHER
, INCREMENT_SALE_INVEST
, INVEST_ACC_CODE
, FUND1_CODE
, FUND1_COST_CODE
, FUND1_AMOUNT
, FUND2_CODE
, FUND2_COST_CODE
, FUND2_AMOUNT
, REMARKS
, FLAG_ROW
, CREATED_BY
, CREATED_DATE
) VALUES (
  @DOC_PROM_PS_ID
, @DOC_PROM_PH_ID
, @DOC_CODE
, @DOC_VER
, @DOC_REV
, @COMP_CODE
, @BRAND_CODE
, @PROD_CODE
, @PROD_SKU
, @ACTIVITY_CODE
, @INVEST_TYPE
, @INVEST_VALUE
, @INVEST_AMOUNT
, @ACTIVITY_CODE_OTHER
, @ACTIVITY_COMBINED_OTHER
, @INCREMENT_SALE_INVEST
, @INVEST_ACC_CODE
, @FUND1_CODE
, @FUND1_CENTER_CODE
, @FUND1_AMOUNT
, @FUND2_CODE
, @FUND2_CENTER_CODE
, @FUND2_AMOUNT
, @REMARKS
, @FLAG_ROW
, @CREATED_BY
, SYSDATETIME()
); SELECT SCOPE_IDENTITY()";

            var parms = new
            {
                DOC_PROM_PS_ID          = entity.DOC_PROM_PS_ID,
                DOC_PROM_PH_ID          = entity.DOC_PROM_PH_ID,
                DOC_CODE                = entity.DOC_CODE,
                DOC_VER                 = entity.DOC_VER,
                DOC_REV                 = entity.DOC_REV,
                COMP_CODE               = entity.COMP_CODE,
                BRAND_CODE              = entity.BRAND_CODE,
                PROD_CODE               = entity.PROD_CODE,
                PROD_SKU                = entity.PROD_SKU,
                ACTIVITY_CODE           = entity.ACTIVITY_CODE,
                INVEST_TYPE             = entity.INVEST_TYPE,
                INVEST_VALUE            = entity.INVEST_VALUE,
                INVEST_AMOUNT           = entity.INVEST_AMOUNT,
                ACTIVITY_CODE_OTHER     = entity.ACTIVITY_CODE_OTHER,
                ACTIVITY_COMBINED_OTHER = entity.ACTIVITY_COMBINED_OTHER,
                INCREMENT_SALE_INVEST   = entity.INCREMENT_SALE_INVEST,
                INVEST_ACC_CODE         = entity.INVEST_ACC_CODE,
                FUND1_CODE              = entity.FUND1_CODE,
                FUND1_COST_CODE         = entity.FUND1_CENTER_CODE,
                FUND1_AMOUNT            = entity.FUND1_AMOUNT,
                FUND2_CODE              = entity.FUND2_CODE,
                FUND2_COST_CODE         = entity.FUND2_CENTER_CODE,
                FUND2_AMOUNT            = entity.FUND2_AMOUNT,
                REMARKS                 = entity.REMARKS,
                FLAG_ROW                = entity.FLAG_ROW.ToString(),
                CREATED_BY              = entity.CREATED_BY
            };

            var DOC_PROM_PI_ID = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );

            entity.DOC_PROM_PI_ID = DOC_PROM_PI_ID;
        }
        public void UpdateInvestment(PromotionPlanngInvestmentDto entity)
        {
            string sqlCommand =
@"UPDATE TB_T_PROMOTION_INVEST
SET
  DOC_PROM_PS_ID          = @DOC_PROM_PS_ID
, DOC_PROM_PH_ID          = @DOC_PROM_PH_ID
, DOC_CODE                = @DOC_CODE
, DOC_VER                 = @DOC_VER
, DOC_REV                 = @DOC_REV
, COMP_CODE               = @COMP_CODE
, BRAND_CODE              = @BRAND_CODE
, PROD_CODE               = @PROD_CODE
, PROD_SKU                = @PROD_SKU
, ACTIVITY_CODE           = @ACTIVITY_CODE
, INVEST_TYPE             = @INVEST_TYPE
, INVEST_VALUE            = @INVEST_VALUE
, INVEST_AMOUNT           = @INVEST_AMOUNT
, ACTIVITY_CODE_OTHER     = @ACTIVITY_CODE_OTHER
, ACTIVITY_COMBINED_OTHER = @ACTIVITY_COMBINED_OTHER
, INCREMENT_SALE_INVEST   = @INCREMENT_SALE_INVEST
, INVEST_ACC_CODE         = @INVEST_ACC_CODE
, FUND1_CODE              = @FUND1_CODE
, FUND1_CENTER_CODE       = @FUND1_CENTER_CODE
, FUND1_AMOUNT            = @FUND1_AMOUNT
, FUND2_CODE              = @FUND2_CODE
, FUND2_CENTER_CODE       = @FUND2_CENTER_CODE
, FUND2_AMOUNT            = @FUND2_AMOUNT
, REMARKS                 = @REMARKS
, FLAG_ROW                = @FLAG_ROW
, UPDATED_BY              = @UPDATED_BY
, UPDATED_DATE            = SYSDATETIME()
WHERE DOC_PROM_PI_ID = @DOC_PROM_PI_ID";

            var parms = new
            {
                DOC_PROM_PI_ID          = entity.DOC_PROM_PI_ID,
                DOC_PROM_PS_ID          = entity.DOC_PROM_PS_ID,
                DOC_PROM_PH_ID          = entity.DOC_PROM_PH_ID,
                DOC_CODE                = entity.DOC_CODE,
                DOC_VER                 = entity.DOC_VER,
                DOC_REV                 = entity.DOC_REV,
                COMP_CODE               = entity.COMP_CODE,
                BRAND_CODE              = entity.BRAND_CODE,
                PROD_CODE               = entity.PROD_CODE,
                PROD_SKU                = entity.PROD_SKU,
                ACTIVITY_CODE           = entity.ACTIVITY_CODE,
                INVEST_TYPE             = entity.INVEST_TYPE,
                INVEST_VALUE            = entity.INVEST_VALUE,
                INVEST_AMOUNT           = entity.INVEST_AMOUNT,
                ACTIVITY_CODE_OTHER     = entity.ACTIVITY_CODE_OTHER,
                ACTIVITY_COMBINED_OTHER = entity.ACTIVITY_COMBINED_OTHER,
                INCREMENT_SALE_INVEST   = entity.INCREMENT_SALE_INVEST,
                INVEST_ACC_CODE         = entity.INVEST_ACC_CODE,
                FUND1_CODE              = entity.FUND1_CODE,
                FUND1_COST_CODE         = entity.FUND1_CENTER_CODE,
                FUND1_AMOUNT            = entity.FUND1_AMOUNT,
                FUND2_CODE              = entity.FUND2_CODE,
                FUND2_COST_CODE         = entity.FUND2_CENTER_CODE,
                FUND2_AMOUNT            = entity.FUND2_AMOUNT,
                REMARKS                 = entity.REMARKS,
                FLAG_ROW                = entity.FLAG_ROW.ToString(),
                UPDATED_BY              = entity.UPDATED_BY
            };

            var effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );

        }
        public void DeleteInvestment(int DOC_PROM_PI_ID)
        {
            string sqlComamnd = @"DELETE TB_T_PROMOTION_INVEST WHERE DOC_PROM_PI_ID = @DOC_PROM_PI_ID;";

            var parms = new
            {
                DOC_PROM_PI_ID = DOC_PROM_PI_ID
            };
            var effected = Connection.ExecuteScalar<int>(
                sql: sqlComamnd,
                param: parms,
                transaction: Transaction
            );
        }

        public IEnumerable<PromotionPlanngSaleDto> GetSaleDataByHeaderID(int DOC_PROM_PH_ID)
        {
            string sqlQuery =
@"SELECT 
  A.DOC_PROM_PS_ID
, A.DOC_PROM_PH_ID
, A.DOC_CODE
, A.DOC_VER
, A.DOC_REV
, A.COMP_CODE
, (SELECT TOP 1 COMP_NAME FROM TB_M_COMPANY WHERE COMP_CODE = A.COMP_CODE) 'COMP_NAME'
, A.BRAND_CODE
, (SELECT TOP 1 BRAND_NAME FROM TB_M_BRAND WHERE BRAND_CODE = A.BRAND_CODE) 'BRAND_NAME'
, A.PROD_CODE
, (SELECT TOP 1 PROD_NAME FROM TB_M_PRODUCT WHERE PROD_CODE = A.PROD_CODE) 'PROD_NAME'
, (SELECT TOP 1 PROD_SKU FROM TB_M_PRODUCT WHERE PROD_CODE = A.PROD_CODE) 'PROD_SKU'
, A.PROD_LTP_EXCL_VAT
, A.NORM_PERC_DISC
, A.NORM_PERC_GP
, A.NORM_SHELF_PRICE
, A.PROMO_PERC_DISC
, A.PROMO_PERC_GP
, A.PROMO_SHELF_PRICE
, A.DEAL_GUIDE_LINE
, A.NET_INTO_STORE
, A.AVG_SALE
, A.AVG_VOLUME
, A.SALE_QTY
, A.SALE_VALUE_EXCL_VAT
, A.SALE_UOM
, A.DISC_TYPE
, A.FLAG_ROW
, A.CREATED_BY
, A.CREATED_DATE
, A.UPDATED_BY
, A.UPDATED_DATE
FROM TB_T_PROMOTION_SALE A
WHERE A.DOC_PROM_PH_ID = @DOC_PROM_PH_ID";

            var parms = new
            {
                DOC_PROM_PH_ID = DOC_PROM_PH_ID
            };

            var query = Connection.Query<PromotionPlanngSaleDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;

        }
        public PromotionPlanngSaleDto GetSaleDataByItemID(int DOC_PROM_PS_ID)
        {
            string sqlQuery =
@"SELECT
  A.DOC_PROM_PS_ID
, A.DOC_PROM_PH_ID
, A.DOC_CODE
, A.DOC_VER
, A.DOC_REV
, A.COMP_CODE
, (SELECT TOP 1 COMP_NAME FROM TB_M_COMPANY WHERE COMP_CODE = A.COMP_CODE) 'COMP_NAME'
, A.BRAND_CODE
, (SELECT TOP 1 BRAND_NAME FROM TB_M_BRAND WHERE BRAND_CODE = A.BRAND_CODE) 'BRAND_NAME'
, A.PROD_CODE
, (SELECT TOP 1 PROD_NAME FROM TB_M_PRODUCT WHERE PROD_CODE = A.PROD_CODE) 'PROD_NAME'
, (SELECT TOP 1 PROD_SKU FROM TB_M_PRODUCT WHERE PROD_CODE = A.PROD_CODE) 'PROD_SKU'
, A.PROD_LTP_EXCL_VAT
, A.NORM_PERC_DISC
, A.NORM_PERC_GP
, A.NORM_SHELF_PRICE
, A.PROMO_PERC_DISC
, A.PROMO_PERC_GP
, A.PROMO_SHELF_PRICE
, A.DEAL_GUIDE_LINE
, A.NET_INTO_STORE
, A.AVG_SALE
, A.AVG_VOLUME
, A.SALE_QTY
, A.SALE_VALUE_EXCL_VAT
, A.SALE_UOM
, A.DISC_TYPE
, A.FLAG_ROW
, A.CREATED_BY
, A.CREATED_DATE
, A.UPDATED_BY
, A.UPDATED_DATE
FROM TB_T_PROMOTION_SALE A
WHERE A.DOC_PROM_PS_ID = @DOC_PROM_PS_ID";

            var parms = new
            {
                DOC_PROM_PS_ID = DOC_PROM_PS_ID
            };

            var query = Connection.QueryFirstOrDefault<PromotionPlanngSaleDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }
        public void InsertPlanngSale(PromotionPlanngSaleDto entity)
        {
            string sqlCommand =
@"INSERT INTO TB_T_PROMOTION_SALE
(
  DOC_PROM_PH_ID
, DOC_CODE
, DOC_VER
, DOC_REV
, COMP_CODE
, BRAND_CODE
, PROD_CODE
, PROD_SKU
, PROD_LTP_EXCL_VAT
, NORM_PERC_DISC
, NORM_PERC_GP
, NORM_SHELF_PRICE
, PROMO_PERC_DISC
, PROMO_PERC_GP
, PROMO_SHELF_PRICE
, DEAL_GUIDE_LINE
, NET_INTO_STORE
, AVG_SALE
, AVG_VOLUME
, SALE_QTY
, SALE_VALUE_EXCL_VAT
, SALE_UOM
, DISC_TYPE
, FLAG_ROW
, CREATED_BY
, CREATED_DATE
) VALUES (
  @DOC_PROM_PH_ID
, @DOC_CODE
, @DOC_VER
, @DOC_REV
, @COMP_CODE
, @BRAND_CODE
, @PROD_CODE
, @PROD_SKU
, @PROD_LTP_EXCL_VAT
, @NORM_PERC_DISC
, @NORM_PERC_GP
, @NORM_SHELF_PRICE
, @PROMO_PERC_DISC
, @PROMO_PERC_GP
, @PROMO_SHELF_PRICE
, @DEAL_GUIDE_LINE
, @NET_INTO_STORE
, @AVG_SALE
, @AVG_VOLUME
, @SALE_QTY
, @SALE_VALUE_EXCL_VAT
, @SALE_UOM
, @DISC_TYPE
, @FLAG_ROW
, @CREATED_BY
, SYSDATETIME()
); SELECT SCOPE_IDENTITY()";

            var parms = new
            {
                DOC_PROM_PH_ID      = entity.DOC_PROM_PH_ID,
                DOC_CODE            = entity.DOC_CODE,
                DOC_VER             = entity.DOC_VER,
                DOC_REV             = entity.DOC_REV,
                COMP_CODE           = entity.COMP_CODE,
                BRAND_CODE          = entity.BRAND_CODE,
                PROD_CODE           = entity.PROD_CODE,
                PROD_SKU            = entity.PROD_SKU,
                PROD_LTP_EXCL_VAT   = entity.PROD_LTP_EXCL_VAT,
                NORM_PERC_DISC      = entity.NORM_PERC_DISC,
                NORM_PERC_GP        = entity.NORM_PERC_GP,
                NORM_SHELF_PRICE    = entity.NORM_SHELF_PRICE,
                PROMO_PERC_DISC     = entity.PROMO_PERC_DISC,
                PROMO_PERC_GP       = entity.PROMO_PERC_GP,
                PROMO_SHELF_PRICE   = entity.PROMO_SHELF_PRICE,
                DEAL_GUIDE_LINE     = entity.DEAL_GUIDE_LINE,
                NET_INTO_STORE      = entity.NET_INTO_STORE,
                AVG_SALE            = entity.AVG_SALE,
                AVG_VOLUME          = entity.AVG_VOLUME,
                SALE_QTY            = entity.SALE_QTY,
                SALE_VALUE_EXCL_VAT = entity.SALE_VALUE_EXCL_VAT,
                SALE_UOM            = entity.SALE_UOM,
                DISC_TYPE           = entity.DISC_TYPE,
                FLAG_ROW            = entity.FLAG_ROW.ToString(),
                CREATED_BY          = entity.CREATED_BY
            };

            var DOC_PROM_PS_ID = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );

            entity.DOC_PROM_PS_ID = DOC_PROM_PS_ID;
        }
        public void UpdatePlanngSale(PromotionPlanngSaleDto entity)
        {
            string sqlCommand =
@"UPDATE TB_T_PROMOTION_SALE
SET
  DOC_PROM_PH_ID      = @DOC_PROM_PH_ID
, DOC_CODE            = @DOC_CODE
, DOC_VER             = @DOC_VER
, DOC_REV             = @DOC_REV
, COMP_CODE           = @COMP_CODE
, BRAND_CODE          = @BRAND_CODE
, PROD_CODE           = @PROD_CODE
, PROD_SKU            = @PROD_SKU
, PROD_LTP_EXCL_VAT   = @PROD_LTP_EXCL_VAT
, NORM_PERC_DISC      = @NORM_PERC_DISC
, NORM_PERC_GP        = @NORM_PERC_GP
, NORM_SHELF_PRICE    = @NORM_SHELF_PRICE
, PROMO_PERC_DISC     = @PROMO_PERC_DISC
, PROMO_PERC_GP       = @PROMO_PERC_GP
, PROMO_SHELF_PRICE   = @PROMO_SHELF_PRICE
, DEAL_GUIDE_LINE     = @DEAL_GUIDE_LINE
, NET_INTO_STORE      = @NET_INTO_STORE
, AVG_SALE            = @AVG_SALE
, AVG_VOLUME          = @AVG_VOLUME
, SALE_QTY            = @SALE_QTY
, SALE_VALUE_EXCL_VAT = @SALE_VALUE_EXCL_VAT
, SALE_UOM            = @SALE_UOM
, DISC_TYPE           = @DISC_TYPE
, FLAG_ROW            = @FLAG_ROW
, UPDATED_BY          = @UPDATED_BY
, UPDATED_DATE        = SYSDATETIME()
WHERE DOC_PROM_PS_ID = @DOC_PROM_PS_ID";

            var parms = new
            {
                DOC_PROM_PS_ID      = entity.DOC_PROM_PS_ID,
                DOC_PROM_PH_ID      = entity.DOC_PROM_PH_ID,
                DOC_CODE            = entity.DOC_CODE,
                DOC_VER             = entity.DOC_VER,
                DOC_REV             = entity.DOC_REV,
                COMP_CODE           = entity.COMP_CODE,
                BRAND_CODE          = entity.BRAND_CODE,
                PROD_CODE           = entity.PROD_CODE,
                PROD_SKU            = entity.PROD_SKU,
                PROD_LTP_EXCL_VAT   = entity.PROD_LTP_EXCL_VAT,
                NORM_PERC_DISC      = entity.NORM_PERC_DISC,
                NORM_PERC_GP        = entity.NORM_PERC_GP,
                NORM_SHELF_PRICE    = entity.NORM_SHELF_PRICE,
                PROMO_PERC_DISC     = entity.PROMO_PERC_DISC,
                PROMO_PERC_GP       = entity.PROMO_PERC_GP,
                PROMO_SHELF_PRICE   = entity.PROMO_SHELF_PRICE,
                DEAL_GUIDE_LINE     = entity.DEAL_GUIDE_LINE,
                NET_INTO_STORE      = entity.NET_INTO_STORE,
                AVG_SALE            = entity.AVG_SALE,
                AVG_VOLUME          = entity.AVG_VOLUME,
                SALE_QTY            = entity.SALE_QTY,
                SALE_VALUE_EXCL_VAT = entity.SALE_VALUE_EXCL_VAT,
                SALE_UOM            = entity.SALE_UOM,
                DISC_TYPE           = entity.DISC_TYPE,
                FLAG_ROW            = entity.FLAG_ROW.ToString(),
                UPDATED_BY          = entity.UPDATED_BY
            };

            var effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }
        public void DeletePlanngSale(int DOC_PROM_PS_ID)
        {
            string sqlComamnd = @"DELETE TB_T_PROMOTION_SALE WHERE DOC_PROM_PS_ID = @DOC_PROM_PS_ID;";

            var parms = new
            {
                DOC_PROM_PS_ID = DOC_PROM_PS_ID
            };
            var effected = Connection.ExecuteScalar<int>(
                sql: sqlComamnd,
                param: parms,
                transaction: Transaction
            );

        }
    }

}
