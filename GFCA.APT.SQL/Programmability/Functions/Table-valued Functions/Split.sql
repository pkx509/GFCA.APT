﻿CREATE FUNCTION [dbo].[Split] (@String VARCHAR (MAX), @Delimiter CHAR (1))
   RETURNS @results TABLE (items VARCHAR (MAX))
AS
   BEGIN
     DECLARE @index   INT
     DECLARE @slice   VARCHAR (8000)
     SELECT @index = 1
     IF len (@String) < 1 OR @String IS NULL
         RETURN
     WHILE @index != 0
     BEGIN
         SET @index = charindex (@Delimiter, @String)
         IF @index != 0
           SET @slice = left (@String, @index - 1)
         ELSE
           SET @slice = @String
         IF (len (@slice) > 0)
           INSERT INTO @results (Items)
           VALUES (@slice)
         SET @String = right (@String, len (@String) - @index)
         IF len (@String) = 0
           BREAK
     END
     RETURN
   END;
GO