CREATE PROCEDURE [dbo].[SP_GENERATE_DOC_CODE]
(
  @IN_DOC_TYPE_CODE T_CODE = 'FC'
, @IN_DOC_YEAR      int = 2021
, @IN_DOC_MONTH     int = 9
, @IN_CUST_CODE     T_CODE
)
as 
BEGIN
set nocount on;

	DECLARE
	  @DOC_CODE T_CODE
	, @DOC_VER int = 0
	, @DOC_REV int = 0

	if @IN_DOC_TYPE_CODE = 'FC'
		begin
			select @DOC_VER = 1, @DOC_REV = 1;

			/* Get Version */
			if exists(select 1 from TB_T_FIXED_CONTRACT_H where DOC_CODE = @DOC_CODE and CUST_CODE = @IN_CUST_CODE)
				begin
					select top 1
					@DOC_VER = DOC_VER + 1
					from TB_T_FIXED_CONTRACT_H 
					where DOC_CODE = @DOC_CODE 
					and CUST_CODE  = @IN_CUST_CODE
					order by DOC_VER desc
				end

			/* Get Revision */
			if exists(select 1 from TB_T_FIXED_CONTRACT_H where DOC_CODE = @DOC_CODE and CUST_CODE = @IN_CUST_CODE and DOC_VER = @DOC_VER)
			begin
				select top 1 
				  @DOC_VER = DOC_VER
				, @DOC_REV = DOC_REV + 1
				from TB_T_FIXED_CONTRACT_H 
				where DOC_CODE = @DOC_CODE 
				and CUST_CODE  = @IN_CUST_CODE
				and DOC_VER = @DOC_VER
				order by DOC_VER desc, DOC_REV desc
			end

		end
	else if @IN_DOC_TYPE_CODE = 'BP'
		begin
			select @DOC_VER = 1, @DOC_REV = 1;
			select @IN_DOC_TYPE_CODE
		end
	else if @IN_DOC_TYPE_CODE = 'SF'
		begin
			select @DOC_VER = 1, @DOC_REV = 1;
			select @IN_DOC_TYPE_CODE
		end
	else if @IN_DOC_TYPE_CODE = 'PP'
		begin
			select @DOC_VER = 1, @DOC_REV = 1;
			
			if exists(select 1 from TB_T_PROMOTION_H where DOC_CODE = @DOC_CODE and CUST_CODE = @IN_CUST_CODE)
			begin
				select top 1
				@DOC_VER = DOC_VER + 1
				from TB_T_PROMOTION_H 
				where DOC_CODE = @DOC_CODE 
				and CUST_CODE  = @IN_CUST_CODE
				order by DOC_VER desc
			end

			if exists(select 1 from TB_T_PROMOTION_H where DOC_CODE = @DOC_CODE and CUST_CODE = @IN_CUST_CODE and DOC_VER = @DOC_VER)
			begin
				select top 1 
				  @DOC_VER = DOC_VER
				, @DOC_REV = DOC_REV + 1
				from TB_T_PROMOTION_H 
				where DOC_CODE = @DOC_CODE 
				and CUST_CODE  = @IN_CUST_CODE
				and DOC_VER = @DOC_VER
				order by DOC_VER desc, DOC_REV desc
			end

		end
	else if @IN_DOC_TYPE_CODE = 'CM'
		begin
			select @DOC_VER = 1, @DOC_REV = 1;
			select @IN_DOC_TYPE_CODE
		end


	set @DOC_CODE = @IN_DOC_TYPE_CODE
	+ '-' + @IN_CUST_CODE
	+ '-' + right('0000'+ convert(varchar(4), @IN_DOC_YEAR), 4) + right('00'+ convert(varchar(2), @IN_DOC_MONTH), 2) 
	+ '-' + right('00'+ convert(varchar(2), @DOC_VER), 2) + right('00'+ convert(varchar(2), @DOC_REV), 2)
	;

	select 
	  @IN_DOC_TYPE_CODE DOC_TYPE_CODE
	, @IN_DOC_YEAR      DOC_YEAR
	, @IN_DOC_MONTH     DOC_MONTH
	, @DOC_CODE      DOC_CODE
	, @DOC_VER	     DOC_VER
	, @DOC_REV	     DOC_REV

		/*
		DOC_TYPE_CODE
        DOC_CODE
        DOC_VER
        DOC_REV
        DOC_MONTH
        DOC_YEAR
        DOC_STATUS
        FLOW_CURRENT
        FLOW_NEXT
        REQUESTER
		*/
END