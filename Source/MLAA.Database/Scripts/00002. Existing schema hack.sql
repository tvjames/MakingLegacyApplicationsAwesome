IF EXISTS (SELECT 1 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' 
           AND TABLE_NAME='Student') BEGIN
  INSERT INTO SchemaVersions(ScriptName, Applied) VALUES ('MLAA.Database.Scripts.00003. Create schema.sql', GETDATE())
END