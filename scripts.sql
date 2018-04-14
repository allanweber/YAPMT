create database yapmt;
use yapmt;

create table Project (
	Id INT AUTO_INCREMENT PRIMARY KEY,
	Name VARCHAR(250) NOT NULL
);


create table Assignment (
	Id INT AUTO_INCREMENT PRIMARY KEY,
	Description VARCHAR(250) NOT NULL,
    User VARCHAR(100) NOT NULL,
    DueDate DATETIME NOT NULL,
    Completed BOOL NOT NULL DEFAULT FALSE,
    DoneDate DATETIME NULL,
    ProjectId INT NOT NULL,
    FOREIGN KEY (ProjectId) REFERENCES Project(Id)
);
