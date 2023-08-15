
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

create table applycompetitorjob(
id UNIQUEIDENTIFIER,
idcompetitor uniqueidentifier,
idjob uniqueidentifier,
idstatus uniqueidentifier,
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

create table englishlevel(
id UNIQUEIDENTIFIER,
namelevel uniqueidentifier,
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);

create table competitor(
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
idrequestjob uniqueidentifier,
idworkexpechistory uniqueidentifier,
iduser uniqueidentifier,
idusercreated uniqueidentifier,
updatedate date, 
createdate date,
isactive bit);

create table experienciework(
id UNIQUEIDENTIFIER,
years integer,
idcompetitor uniqueidentifier,
namecompany varchar(40), 
position varchar(40),
reasonforresignation varchar(max),
initdate date,
finalizationdate date,
namedirectboss varchar(40),
idusercreated uniqueidentifier,
updatedate date, 
createdate date);
