create table ItemCode (
ItemCodeID uniqueidentifier not null primary key ,
ItemCode nvarchar(50) null,
ItemName varchar(100) null
);
alter table Items add ItemCodeID uniqueidentifier null  foreign key references ItemCode(ItemCodeID)