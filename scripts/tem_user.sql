USE [remboard2]
GO

INSERT INTO [dbo].[ProjectRole]
           ([Id]
           ,[Code]
           ,[Name])
  select 1,'Admin','Администратор'
  union all
   select 2,'Manager','Менеджер'
  union all
   select 3,'Engineer','Инженер'


   go
   USE [remboard2]
GO

declare @tenantid uniqueidentifier 

set @tenantid = NEWID()

INSERT INTO [dbo].[Tenant]
           ([Id]
           ,[IsDeleted]
           ,[DateCreated]
           ,[DateModified]
           ,[RegistredEmail]
           ,[IsActive]
           ,[LegalName]
           ,[Trademark]
           ,[Address]
           ,[UserLogin])

		   select @tenantid,0,GETDATE(),null,'a@a.ru',1,'test','test','addr','test'






INSERT INTO [dbo].[User]
           ([Id]
           ,[IsDeleted]
           ,[DateCreated]
           ,[DateModified]
           ,[TenantId]
           ,[ProjectRoleId]
           ,[LoginName]
           ,[FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[Phone]
           ,[Email])

		   select NEWID(),0,getdate(),null,@tenantid,1,'test','1','2','3','77777','aa@ya.ru'
     
GO




