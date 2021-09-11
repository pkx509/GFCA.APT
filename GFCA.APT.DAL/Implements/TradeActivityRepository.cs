using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GFCA.APT.DAL.Implements
{
    public class TradeActivityRepository : RepositoryBase, ITradeActivityRepository
    {
        public TradeActivityRepository(IDbTransaction transaction) : base(transaction) { }
        
        public IEnumerable<TradeActivityDto> All()
        {
            try
            {

                string sqlQuery = @"SELECT * FROM TB_M_ACTIVITY;";
                var query = Connection.Query<TB_M_ACTIVITY>(
                    sql: sqlQuery
                    , transaction: Transaction
                    );

                return query
                    .Select(q => q.ToDto())
                    .AsEnumerable<TradeActivityDto>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TradeActivityDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM TB_M_ACTIVITY WHERE ACTIVITY_ID = @ACTIVITY_ID;";

            try
            {

                var parms = new
                {
                    ACTIVITY_ID = id
                };
                var query = Connection.Query<TB_M_ACTIVITY>(
                    sql: sqlQuery
                    , param: parms
                    , transaction: Transaction
                    ).FirstOrDefault();

                return query.ToDto();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(TradeActivityDto dto)
        {
            string sqlExecute = @"INSERT INTO TB_M_ACTIVITY 
                                (
                                      ACC_ID
                                    , ACTIVITY_CODE
                                    , ACTIVITY_TYPE
                                    , ACTIVTITY_NAME
                                    , HAS_FIXED_CONTRACT
                                    , CAN_DEDUCTABLE
                                    , IN_THB_CS
                                    , IN_GROSS_SALE
                                    , IN_NOT_SALE
                                    , OUT_THB_CS
                                    , OUT_GROSS_SALE
                                    , OUT_NOT_SALE
                                    , NO_RELATE_ABS_AMT
                                    , VALUABLE
                                    , ACTIVITY_DESC
                                    , FLAG_ROW
                                    , CREATED_BY
                                    , CREATED_DATE
                                ) 
                                VALUES 
                                (
                                      @ACC_ID
                                    , @ACTIVITY_CODE
                                    , @ACTIVITY_TYPE
                                    , @ACTIVTITY_NAME
                                    , @HAS_FIXED_CONTRACT
                                    , @CAN_DEDUCTABLE
                                    , @IN_THB_CS
                                    , @IN_GROSS_SALE
                                    , @IN_NOT_SALE
                                    , @OUT_THB_CS
                                    , @OUT_GROSS_SALE
                                    , @OUT_NOT_SALE
                                    , @NO_RELATE_ABS_AMT
                                    , @VALUABLE
                                    , @ACTIVITY_DESC
                                    , @FLAG_ROW
                                    , @CREATED_BY
                                    , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()";

            try
            {

                var entity = dto.ToEntity();
                var parms = new
                {
                      ACTIVITY_ID        = entity.ACTIVITY_ID
                    , ACC_ID             = entity.ACC_ID
                    , ACTIVITY_CODE      = entity.ACTIVITY_CODE
                    , ACTIVITY_TYPE      = entity.ACTIVITY_TYPE
                    , ACTIVTITY_NAME     = entity.ACTIVTITY_NAME
                    , HAS_FIXED_CONTRACT = entity.HAS_FIXED_CONTRACT
                    , CAN_DEDUCTABLE     = entity.CAN_DEDUCTABLE
                    , IN_THB_CS          = entity.IN_THB_CS
                    , IN_GROSS_SALE      = entity.IN_GROSS_SALE
                    , IN_NOT_SALE        = entity.IN_NOT_SALE
                    , OUT_THB_CS         = entity.OUT_THB_CS
                    , OUT_GROSS_SALE     = entity.OUT_GROSS_SALE
                    , OUT_NOT_SALE       = entity.OUT_NOT_SALE
                    , NO_RELATE_ABS_AMT  = entity.NO_RELATE_ABS_AMT
                    , VALUABLE           = entity.VALUABLE
                    , ACTIVITY_DESC      = entity.ACTIVITY_DESC
                    , FLAG_ROW           = entity.FLAG_ROW
                    , CREATED_BY         = entity.CREATED_BY??""
                    , CREATED_DATE       = System.DateTime.Now.ToDateTime2()
                    //, UPDATED_BY       = entity.UPDATED_BY
                    //, UPDATED_DATE     = entity.UPDATED_DATE
                };

                Connection.ExecuteScalar<int>(
                    sql: sqlExecute
                    , param: parms
                    , transaction: Transaction
                );

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(TradeActivityDto dto)
        {
            string sqlExecute = @"UPDATE TB_M_ACTIVITY
                                SET
                                      ACC_ID             = @ACC_ID
                                    , ACTIVITY_CODE      = @ACTIVITY_CODE
                                    , ACTIVITY_TYPE      = @ACTIVITY_TYPE
                                    , ACTIVTITY_NAME     = @ACTIVTITY_NAME
                                    , HAS_FIXED_CONTRACT = @HAS_FIXED_CONTRACT
                                    , CAN_DEDUCTABLE     = @CAN_DEDUCTABLE
                                    , IN_THB_CS          = @IN_THB_CS
                                    , IN_GROSS_SALE      = @IN_GROSS_SALE
                                    , IN_NOT_SALE        = @IN_NOT_SALE
                                    , OUT_THB_CS         = @OUT_THB_CS
                                    , OUT_GROSS_SALE     = @OUT_GROSS_SALE
                                    , OUT_NOT_SALE       = @OUT_NOT_SALE
                                    , NO_RELATE_ABS_AMT  = @NO_RELATE_ABS_AMT
                                    , VALUABLE           = @VALUABLE
                                    , ACTIVITY_DESC      = @ACTIVITY_DESC
                                    , FLAG_ROW           = @FLAG_ROW
                                    , UPDATED_BY         = @UPDATED_BY
                                    , UPDATED_DATE       = @UPDATED_DATE
                                WHERE ACTIVITY_ID = @ACTIVITY_ID;";
            try
            {

                var entity = dto.ToEntity();
                var parms = new
                {
                    ACTIVITY_ID = entity.ACTIVITY_ID
                    , ACC_ID = entity.ACC_ID
                    , ACTIVITY_CODE = entity.ACTIVITY_CODE
                    , ACTIVITY_TYPE = entity.ACTIVITY_TYPE
                    , ACTIVTITY_NAME = entity.ACTIVTITY_NAME
                    , HAS_FIXED_CONTRACT = entity.HAS_FIXED_CONTRACT
                    , CAN_DEDUCTABLE = entity.CAN_DEDUCTABLE
                    , IN_THB_CS = entity.IN_THB_CS
                    , IN_GROSS_SALE = entity.IN_GROSS_SALE
                    , IN_NOT_SALE = entity.IN_NOT_SALE
                    , OUT_THB_CS = entity.OUT_THB_CS
                    , OUT_GROSS_SALE = entity.OUT_GROSS_SALE
                    , OUT_NOT_SALE = entity.OUT_NOT_SALE
                    , NO_RELATE_ABS_AMT = entity.NO_RELATE_ABS_AMT
                    , VALUABLE = entity.VALUABLE
                    , ACTIVITY_DESC = entity.ACTIVITY_DESC
                    , FLAG_ROW = entity.FLAG_ROW
                    , UPDATED_BY = entity.UPDATED_BY
                    , UPDATED_DATE = System.DateTime.Now.ToDateTime2()
                };

                Connection.ExecuteScalar<int>(
                    sql: sqlExecute
                    , param: parms
                    , transaction: Transaction
                );

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            string sqlExecute = @"DELETE FROM TB_M_ACTIVITY WHERE ACTIVITY_ID = @ACTIVITY_ID;";

            try
            {
                var parms = new
                {
                     ACTIVITY_ID = id
                };
                Connection.ExecuteScalar<int>(
                    sql: sqlExecute
                    , param: parms
                    , transaction: Transaction
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    internal static class TradeActivityRepositoryExtension
    {

        public static TB_M_ACTIVITY ToEntity(this TradeActivityDto self)
        {
            var entity = new TB_M_ACTIVITY();
            try
            {

                entity.ACTIVITY_ID         = self.ACTIVITY_ID??0;
                entity.ACC_ID              = self.ACC_ID;
                entity.ACTIVITY_CODE       = self.ACTIVITY_CODE;
                entity.ACTIVITY_TYPE       = self.ACTIVITY_TYPE;
                entity.ACTIVTITY_NAME      = self.ACTIVTITY_NAME;

                entity.HAS_FIXED_CONTRACT  = self.HAS_FIXED_CONTRACT? "Y": null;
                entity.CAN_DEDUCTABLE      = self.CAN_DEDUCTABLE ? "Y" : null;
                entity.IN_THB_CS           = self.IN_THB_CS ? "Y" : null;
                entity.IN_GROSS_SALE       = self.IN_GROSS_SALE ? "Y" : null;
                entity.IN_NOT_SALE         = self.IN_NOT_SALE ? "Y" : null;
                entity.OUT_THB_CS          = self.OUT_THB_CS ? "Y" : null;
                entity.OUT_GROSS_SALE      = self.OUT_GROSS_SALE ? "Y" : null;
                entity.OUT_NOT_SALE        = self.OUT_NOT_SALE ? "Y" : null;
                entity.NO_RELATE_ABS_AMT   = self.NO_RELATE_ABS_AMT ? "Y" : null;

                entity.VALUABLE            = self.VALUABLE;
                entity.ACTIVITY_DESC       = self.ACTIVITY_DESC;
                entity.FLAG_ROW            = self.FLAG_ROW;
                entity.CREATED_BY          = self.CREATED_BY;
                entity.CREATED_DATE        = self.CREATED_DATE.GetValueOrDefault().ToDateTime2();
                entity.UPDATED_BY          = self.CREATED_BY;
                entity.UPDATED_DATE        = self.CREATED_DATE.GetValueOrDefault().ToDateTime2();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity;
        }
        public static TradeActivityDto ToDto(this TB_M_ACTIVITY self)
        {
            var dto = new TradeActivityDto();
            try
            {

                dto.ACTIVITY_ID        = self.ACTIVITY_ID;
                dto.ACC_ID             = self.ACC_ID;
                dto.ACTIVITY_CODE      = self.ACTIVITY_CODE;
                dto.ACTIVITY_TYPE      = self.ACTIVITY_TYPE;
                dto.ACTIVTITY_NAME     = self.ACTIVTITY_NAME;

                dto.HAS_FIXED_CONTRACT = self.HAS_FIXED_CONTRACT == "Y" ? true : false;
                dto.CAN_DEDUCTABLE     = self.CAN_DEDUCTABLE == "Y" ? true : false;
                dto.IN_THB_CS          = self.IN_THB_CS == "Y" ? true : false;
                dto.IN_GROSS_SALE      = self.IN_GROSS_SALE == "Y" ? true : false;
                dto.IN_NOT_SALE        = self.IN_NOT_SALE == "Y" ? true : false;
                dto.OUT_THB_CS         = self.OUT_THB_CS == "Y" ? true : false;
                dto.OUT_GROSS_SALE     = self.OUT_GROSS_SALE == "Y" ? true : false;
                dto.OUT_NOT_SALE       = self.OUT_NOT_SALE == "Y" ? true : false;
                dto.NO_RELATE_ABS_AMT  = self.NO_RELATE_ABS_AMT == "Y" ? true : false;

                dto.VALUABLE           = self.VALUABLE;
                dto.ACTIVITY_DESC      = self.ACTIVITY_DESC;
                dto.FLAG_ROW           = self.FLAG_ROW;
                dto.CREATED_BY         = self.CREATED_BY;
                dto.CREATED_DATE       = self.CREATED_DATE;
                dto.UPDATED_BY         = self.CREATED_BY;
                dto.UPDATED_DATE       = self.CREATED_DATE;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dto;
        }
    
    }
}
