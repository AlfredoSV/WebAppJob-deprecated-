CREATE DATABASE dbjob;
GO
USE dbJOB;
GO

create table job(
id UNIQUEIDENTIFIER,
namejob varchar(30), 
idcompany uniqueidentifier,
salarymax decimal,
salarymin decimal,
vacancynumbers int,
idarea uniqueidentifier,
descriptionjob varchar(max),
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);

create table company(
id UNIQUEIDENTIFIER,
namecompany varchar(30), 
descriptioncompany varchar(max),
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);

create table area(
id UNIQUEIDENTIFIER,
namearea varchar(30), 
descriptionarea varchar(max),
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);


