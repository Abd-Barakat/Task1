select * from questions;
select * from Slider;
select * from Stars;
select * from Smiley;




drop table questions ;
drop table Slider;
drop table Smiley;
drop table Stars;


create table questions (
Question_text varchar(255),
Question_order int ,
Question_type varchar (255),
Id int IDENTITY(0,1) not null ,
primary key (id)

);


create table Smiley (
Id int IDENTITY (0,1)  not null,
Question_order int ,
Num_Faces int,
Question_Id int,
primary key (Id),
check (Num_Faces between 2 and 5),
foreign key (Question_Id)references questions(Id)
On delete cascade 
On update cascade
);




Create table Slider (
Id int IDENTITY (0,1)  not null,
Question_order int ,
Start_Value int ,
End_Value int ,
Start_Value_Caption varchar (255),
End_Value_Caption varchar (255),
Question_Id int,
check (Start_Value between 0 and 100),
check (Start_Value <End_Value),
check (End_Value between 0 and 100),
primary key (Id),
foreign key (Question_Id) references  questions(Id)
On delete cascade 
On update cascade

);



create table Stars (
Id int IDENTITY (0,1)  not null,
question_order int ,
Num_Stars int ,
Question_Id int,
primary key(Id),
check (Num_Stars between 0 and 10),
foreign key (Question_Id)references  questions(Id)
on delete cascade
On update cascade

);



insert into questions values ('First Question' ,1,'Smiley');
insert into questions values ('Second Question' ,2,'Stars');
insert into questions values ('Third Question' ,3,'Slider');
insert into questions values ('Forth Question' ,4,'Smiley');

insert into Smiley values (1 ,2,0);
insert into Smiley values (4 ,4,3);

insert into Slider values (3,0,100,'Not satisfied','Extremly satisfied',3);

insert into Stars values (2,5,1);


delete from questions where id=3;
go

create proc Reset_IDs
as
DBCC CHECKIDENT ('questions', RESEED,-1) 
DBCC CHECKIDENT('Slider',RESEED,-1);
DBCC CHECKIDENT('Stars',RESEED,-1);
DBCC CHECKIDENT('Smiley',RESEED,-1);
return 
Go

--select  questions.Question_order ,Question_text from questions inner join Smiley on questions.id = Smiley.Question_Id where questions.id=3
--Go

--EXECUTE sys.sp_rename  --Rename database in @objname to @newname
--    @objname = N'dbo.questions_switch',
--    @newname = N'questions',
--    @objtype = 'OBJECT';
--Go 

create procedure Insert_Question
@Text varchar(2000),
@Order int ,
@Type varchar(50) 
As 
insert into questions values (@Text,@Order,@Type);
return
Go

create procedure Update_Question
@Text varchar(2000),
@Order int ,
@Type varchar(50) ,
@Q_Id int 
As 
update questions set Question_text =@Text,Question_order=@Order,Question_type=@Type where Id = @Q_Id
return
Go

alter procedure Insert_Slider
@Order int ,
@Start int ,
@End int ,
@Start_Caption varchar(255),
@End_Caption varchar(255)
As
declare @Q_Id int 
select @Q_Id=max(Id) from questions 
insert into Slider values (@Order,@Start,@End,@Start_Caption,@End_Caption,@Q_Id);
return 
Go

create procedure Update_Slider
@Order int ,
@Start int ,
@End int ,
@Start_Caption varchar(255),
@End_Caption varchar(255),
@Q_Id int 
As
Update Slider set Question_order=@Order,Start_Value=@Start,End_Value=@End,Start_Value_Caption=@Start_Caption,End_Value_Caption=@End_Caption where Question_Id=@Q_Id
return 
Go

alter procedure Insert_Smiley
@Order int ,
@Num_Faces int 
As 
declare @Q_Id int 
select @Q_Id=max(Id) from questions 
insert into Smiley values (@Order,@Num_Faces,@Q_Id);
return 
Go

create procedure Update_Smiley
@Order int ,
@Num_Faces int ,
@Q_Id int
As 
Update Smiley set Question_order=@Order,Num_Faces=@Num_Faces where Question_Id=@Q_Id
return 
Go

alter procedure Insert_Stars
@Order int ,
@Num_Stars int 
As 
declare @Q_Id int 
select @Q_Id=max(Id) from questions 
insert into Stars values (@Order,@Num_Stars,@Q_Id);
return 
Go

create procedure Update_Stars
@Order int ,
@Num_Stars int,
@Q_Id int
As 
Update Stars set Question_order =@Order,Num_Stars=@Num_Stars where Question_Id=@Q_Id
return 
Go

Alter procedure delete_question 
@Id int 
As 
begin
declare @command nvarchar (255)
set @command ='delete from questions where Id ='+CAST( @Id As varchar(255))
execute sp_executesql @command
return 
end
Go

--exec delete_question @id =2 
--Go

--select * from questions;
--Go


delete from questions where id =2
DBCC CHECKIDENT ('questions', RESEED,-1) 
select * from questions