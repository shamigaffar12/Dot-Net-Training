use Assignments

---Assignments 4----
---1.Write a T-SQL Program to find the factorial of a given number.
begin transaction
declare @num int=5,
@factNum int =1 ,@result int=1

if @num<0
begin
print 'Cannot Find the Factorial for negative number'
rollback transaction;
return;
end

if @num=0
begin
print 'Factorial of 0 is 1'
end

else
begin
while @result<=@num
begin
set @factNum=@factNum*@result;
set @result=@result+1;
end
print 'Factorial of ' + cast(@num as varchar) + ' is ' + cast(@factNum as varchar)
end
commit transaction
---------------------------------------------------------------------------------------

--Q2.Create a stored procedure to generate multiplication table that
--accepts a number and generates up to a given number. 

create or alter proc MultTable @num int,  @Lastnum int

as
begin
begin transaction
 if(@num <= 0 or @Lastnum <=0)
 begin
  print 'Enter a positive number'
  rollback transaction;
  return;
 end
 
  declare @mult int = 1

 while(@mult <= @Lastnum)
 begin
 print cast(@num as varchar) + ' * ' + cast(@mult as varchar) + ' = ' + cast(@num * @Lastnum as varchar);
 set @Lastnum = @Lastnum+1;
 end

commit transaction
end
--call the procedure and give the input of  num and lastnum
 exec MultTable 3 ,10

 --Q3Create a function to calculate the status of the student. 
 --If student score >=50 then pass, else fail. Display the data neatly

 -- Creating a function for checking result status
create or alter function Studentstatus(@Score int)
returns varchar(20)
as
begin
declare @result varchar(20)
if(@score >= 50)
set @result = 'Pass'
else
set @result = 'Fail'
return @result
end
-----create table students----
create table students
(Sid int primary key,
Sname varchar(40))

---insert record into student table
insert into students values(1, 'Jack'),
                           (2, 'Rithvik'),
                           (3, 'Jaspreeth'),
                           (4, 'Praveen'),
                           (5, 'Bisa'),
                           (6, 'Suraj')
----display table student--------------
select * from students
---create table marks---
create table marks (Mid int primary key,
Sid int references students(Sid),Score int)
----insert record into table marks----------

insert into marks values (1, 1, 23),
                         (2, 6, 95),
                         (3, 4, 98),
                         (4, 2, 17),
                         (5, 3, 53),
                         (6, 5, 13)
--display marks table-------------
select * from marks
 -----calling Studentstatus function to check student result is pass or not
 select student.sid, student.sname, mark.score, dbo.Studentstatus(mark.score)
 as 'Student Result Status' from students student join marks mark on student.sid = mark.sid 
