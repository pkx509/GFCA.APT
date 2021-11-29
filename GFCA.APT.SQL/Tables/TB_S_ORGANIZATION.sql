CREATE TABLE [dbo].[TB_S_ORGANIZATION](
	[COMP_CODE] [dbo].[T_CODE] NULL,
	[ORG_CODE] [dbo].[T_CODE] NOT NULL,
	[REPORT_TO] [dbo].[T_CODE] NULL,
	[HIERACHY_ID] [dbo].[T_DESC] NULL,
	[ORG_NAME] [varchar](100) NULL,
	[FLAG_ORG] [dbo].[T_FLAG] NULL,
	[FLAG_ROW] [dbo].[T_FLAG] NULL,
	[UPLOAD_BY] [dbo].[T_NAME] NULL,
	[UPLOAD_DATE] [dbo].[T_DATETIME] NULL
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

--C = Company | D = Division | S = Sub-Division | R = Department | T = Sub-Department | P = Position
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