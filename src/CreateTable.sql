Drop table States
GO
Drop Table Countries
GO
CREATE TABLE Countries (
    Id int not null Identity(1,1) Primary key,
    Name nvarchar(100) not null,
    Code varchar(10) not null,
    Status tinyint not null,
	CreatedBy int not null,
	CreatedTime DateTime not null,
	UpdatedBy int,
	UpdatedTime DateTime,
	RowVersion Timestamp
)
GO

CREATE TABLE States (
    Id int not null Identity(1,1) Primary key,
	CountryId int not null,
    Name nvarchar(100) not null,
    Code varchar(10) not null,
    Status tinyint not null,
	CreatedBy int not null,
	CreatedTime DateTime not null,
	UpdatedBy int,
	UpdatedTime DateTime,
	RowVersion Timestamp,
	--CONSTRAINT FK_PersonOrder FOREIGN KEY (CountryId)
 --   REFERENCES Countries(Id)
)
