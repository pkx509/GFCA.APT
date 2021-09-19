CREATE TABLE [dbo].[TB_S_ORGANIZATION](
	[COMP_CODE] [varchar](50) NULL,
	[ORG_CODE] [varchar](50) NOT NULL,
	[REPORT_TO] [varchar](50) NULL,
	[HIERACHY_ID] [varchar](max) NULL,
	[ORG_NAME] [varchar](100) NULL,
	[FLAG_ORG] [varchar](1) NULL,
	[FLAG_ROW] [varchar](1) NULL,
	[UPLOAD_BY] [varchar](100) NULL,
	[UPLOAD_DATE] [datetime2](7) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_S_ORGANIZATION] ADD  CONSTRAINT [DF_TB_S_ORGANIZATION_FLAG_ORG]  DEFAULT ('P') FOR [FLAG_ORG]
GO

ALTER TABLE [dbo].[TB_S_ORGANIZATION] ADD  CONSTRAINT [DF_TB_S_ORGANIZATION_FLAG_ROW]  DEFAULT ('I') FOR [FLAG_ROW]
GO

ALTER TABLE [dbo].[TB_S_ORGANIZATION] ADD  CONSTRAINT [DF_TB_S_ORGANIZATION_UPLOAD_BY]  DEFAULT ('System') FOR [UPLOAD_BY]
GO

ALTER TABLE [dbo].[TB_S_ORGANIZATION] ADD  CONSTRAINT [DF_TB_S_ORGANIZATION_UPLOAD_DATE]  DEFAULT (sysdatetime()) FOR [UPLOAD_DATE]
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'P = Position | D = Department',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TB_S_ORGANIZATION',
    @level2type = N'COLUMN',
    @level2name = N'FLAG_ORG'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'I = Insert | else update',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TB_S_ORGANIZATION',
    @level2type = N'COLUMN',
    @level2name = N'FLAG_ROW'