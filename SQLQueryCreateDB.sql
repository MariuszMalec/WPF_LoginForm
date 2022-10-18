create database MvvmLoginDb
go
use MvvmLoginDb

go
create table [User]
(
	Id UNIQUEIDENTIFIER primary Key default NEWID(),
	UserName nvarchar (50) unique not null,
	[Password] nvarchar (100) not null,
	[Name] nvarchar (50) not null,
	LastName nvarchar (50) not null,
	[Email] nvarchar (100) unique not null
)
go
insert into [User] values (NEWID(), 'admin' , 'admin', 'Mario','Bros','mm@example.com')
go

select *from [User]