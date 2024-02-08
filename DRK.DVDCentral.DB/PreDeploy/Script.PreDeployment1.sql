/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
drop table if exists tblCustomer
drop table if exists tblDirector
drop table if exists tblFormat
drop table if exists tblGenre
drop table if exists tblMovie
drop table if exists tblMovieGenre
drop table if exists tblOrder
drop table if exists tblOrderItem
drop table if exists tblRating
drop table if exists tblUser