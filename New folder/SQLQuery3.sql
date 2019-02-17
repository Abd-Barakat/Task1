create table Smiley 
(
Question varchar (255),
number int ,
rate int ,
check (rate between 2 and 5),
primary key (number)
 
);


create table  	Slider 
(
Question varchar (255),
Number int ,
Start_Value int ,
End_Value int, 
Start_Value_Caption int ,
End_Value_Caption int, 
check (Start_Value between 0 and 100),
check (End_Value between 0 and 100),
check (Start_Value_Caption between 0 and 100),
check (End_Value_Caption between 0 and 100),
primary key (number)
);


create table Stars  
(
Question varchar (255),
number int ,
rate int ,
check (rate between 0 and 10),
primary key (number)
 
);


insert into Smiley Values ('First Question' ,1,3);
insert into Smiley Values ('Second Question' ,2,3);
insert into Smiley Values ('Third Question' ,3,3);
insert into Smiley Values ('Forth Question' ,4,3);


insert into Slider Values ('First Question' ,1,0,100,50,60);
insert into Slider Values ('First Question' ,2,3,100,40,60);



insert into Stars Values ('First Question' ,1,3);
insert into Stars Values ('Second Question' ,2,1);
insert into Stars Values ('Third Question' ,3,5);
insert into Stars Values ('Forth Question' ,4,0);

select * 
from Stars;

select * 
from Slider;

select * 
from Smiley;