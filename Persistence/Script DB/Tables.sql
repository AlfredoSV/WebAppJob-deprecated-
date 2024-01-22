
CREATE DATABASE dbjob;
GO
USE dbJOB;
GO

---Tables

create table jobs(
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
isactive bit,
tags varchar(500),
logo varchar(max));

create table companies(
id UNIQUEIDENTIFIER,
namecompany varchar(30) not null, 
descriptioncompany varchar(max) not null,
idusercreated uniqueidentifier not null,
updatedate date not null, 
createdate date not null,
isactive bit not null,
PRIMARY KEY(id));

create table areas(
id UNIQUEIDENTIFIER,
namearea varchar(30) not null, 
descriptionarea varchar(max) not null,
idusercreated uniqueidentifier not null,
updatedate date not null, 
createdate date not null,
isactive bit not null,
PRIMARY KEY(id));

create table applycompetitorjobs(
id UNIQUEIDENTIFIER,
idcompetitor uniqueidentifier,
idjob uniqueidentifier,
idstatus uniqueidentifier,
folio varchar(30) not null,
dateapply date,
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);

create table statusgeneral(
id UNIQUEIDENTIFIER,
namestatus uniqueidentifier,
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);

create table civilstatus(
id UNIQUEIDENTIFIER,
namestatus uniqueidentifier,
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);

create table englishlevels(
id UNIQUEIDENTIFIER,
namelevel uniqueidentifier,
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);

create table competitors(
id UNIQUEIDENTIFIER,
namecompetitor varchar(30), 
lastnamecompetitor varchar(30),
birthdaydate date,
age int,
idenglishlevel uniqueidentifier,
cv varchar(max),
yearsofexperience int,
idlastgradestudies uniqueidentifier,
idcivilstatus uniqueidentifier,
iduser uniqueidentifier,
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);

create table experienciework(
id UNIQUEIDENTIFIER,
years integer,
months integer,
idcompetitor uniqueidentifier,
namecompany varchar(40), 
position varchar(40),
reasonforresignation varchar(max),
initdate date,
finalizationdate date,
namedirectboss varchar(40),
numbercontact varchar(30),
idusercreated uniqueidentifier,
updatedate date, 
createdate date);


create table countries(
	
	id uniqueidentifier not null,
	countryName varchar(50) not null,
	countryDescription varchar(50) not null,
	datecreated datetime not null,
	active bit not null

);

--INSERT INTO countries VALUES(NEWID(), 'México', 'México City', getdate(), 1);

--SELECT * FROM countries