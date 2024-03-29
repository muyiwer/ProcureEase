SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE
WHERE CONSTRAINT_NAME LIKE 'FK__UserProfi__Depar%'
OR CONSTRAINT_NAME LIKE 'FK_Procurements_Department%'
OR CONSTRAINT_NAME LIKE 'FK__Items__ItemCodeI%'
OR CONSTRAINT_NAME LIKE 'FK__Items__Procureme%'
OR CONSTRAINT_NAME LIKE 'FK_SourceOfFundsOrganizationSettings_SourceOfFunds%'
OR CONSTRAINT_NAME LIKE 'FK_ProcurementMethodOrganizationSettings_ProcurementMethod%'
OR CONSTRAINT_NAME LIKE 'FK_ProjectCategoryOrganizationSettings_ProjectCategory%'
GROUP BY CONSTRAINT_NAME




ALTER TABLE [dbo].[Items] DROP CONSTRAINT FK__Items__ItemCodeI__04E4BC85
ALTER TABLE [dbo].[Items] DROP CONSTRAINT FK__Items__ItemCodeI__208CD6FA
ALTER TABLE [dbo].[Items] DROP CONSTRAINT FK__Items__Procureme__49C3F6B7
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT FK__UserProfi__Depar__123EB7A3
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT FK__UserProfi__Depar__367C1819
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] DROP CONSTRAINT FK_ProcurementMethodOrganizationSettings_ProcurementMethod
ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT FK_Procurements_Department
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] DROP CONSTRAINT FK_ProjectCategoryOrganizationSettings_ProjectCategory
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] DROP CONSTRAINT FK_SourceOfFundsOrganizationSettings_SourceOfFunds




ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK__UserProfi__Depar__123EB7A3]

GO

ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK__UserProfi__Depar__123EB7A3] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK__UserProfi__Depar__123EB7A3]
GO


ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT [FK_Procurements_Department]

GO

ALTER TABLE [dbo].[Procurements]  WITH CHECK ADD  CONSTRAINT [FK_Procurements_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[Procurements] CHECK CONSTRAINT [FK_Procurements_Department]
GO


ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK__Items__ItemCodeI__04E4BC85]

GO

ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK__Items__ItemCodeI__04E4BC85] FOREIGN KEY([ItemCodeID])
REFERENCES [dbo].[ItemCode] ([ItemCodeID])
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK__Items__ItemCodeI__04E4BC85]
GO



ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK__Items__Procureme__49C3F6B7]

GO

ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK__Items__Procureme__49C3F6B7] FOREIGN KEY([ProcurementID])
REFERENCES [dbo].[Procurements] ([ProcurementID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK__Items__Procureme__49C3F6B7]
GO



ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] DROP CONSTRAINT [FK_SourceOfFundsOrganizationSettings_SourceOfFunds]

GO

ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_SourceOfFundsOrganizationSettings_SourceOfFunds] FOREIGN KEY([SourceOfFundID])
REFERENCES [dbo].[SourceOfFunds] ([SourceOfFundID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] CHECK CONSTRAINT [FK_SourceOfFundsOrganizationSettings_SourceOfFunds]
GO



ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] DROP CONSTRAINT [FK_ProcurementMethodOrganizationSettings_ProcurementMethod]

GO

ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementMethodOrganizationSettings_ProcurementMethod] FOREIGN KEY([ProcurementMethodID])
REFERENCES [dbo].[ProcurementMethod] ([ProcurementMethodID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementMethodOrganizationSettings_ProcurementMethod]
GO



ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] DROP CONSTRAINT [FK_ProjectCategoryOrganizationSettings_ProjectCategory]

GO

ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCategoryOrganizationSettings_ProjectCategory] FOREIGN KEY([ProjectCategoryID])
REFERENCES [dbo].[ProjectCategory] ([ProjectCategoryID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] CHECK CONSTRAINT [FK_ProjectCategoryOrganizationSettings_ProjectCategory]
GO


