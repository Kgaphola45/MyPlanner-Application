
CREATE DATABASE STUDY_PLANNER;
CREATE TABLE dbo.module (
 
id INT IDENTITY(1,1) PRIMARY KEY,
 
stdNumber VARCHAR(255) NOT NULL,
 
module_code VARCHAR(255) NOT NULL,
 
module_name VARCHAR(255) NOT NULL,
 
credits INT NOT NULL,
 
class_hours INT NOT NULL,
 
semester_weeks INT NOT NULL,
);


CREATE TABLE dbo.hours (
 
id INT IDENTITY(1,1) PRIMARY KEY,
 
stdNumber VARCHAR(255) NOT NULL,
 
module_code VARCHAR(255) NOT NULL,
 
self_study INT NOT NULL,
 
hours_remain INT NOT NULL,
 
);


CREATE TABLE dbo.student (
 
id INT IDENTITY(1,1) PRIMARY KEY,
 
stdNumber VARCHAR(255) NOT NULL,
 
password VARCHAR(255) NOT NULL
);

