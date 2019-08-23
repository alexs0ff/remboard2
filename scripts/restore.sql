USE [master];

DECLARE @kill varchar(8000) = '';  
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
FROM sys.dm_exec_sessions
WHERE database_id  = db_id('remboard2')

EXEC(@kill);


USE [master]
RESTORE DATABASE [remboard2] FROM  DISK = N'E:\Backups\remboard2_8.22.2019.bak' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 5

GO


