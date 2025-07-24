use assignments

-----Assignment 5-------------------------
/* 1. Write a T-Sql based procedure to generate complete payslip
    of a given employee with respect to the following condition

   a) HRA as 10% of Salary
   b) DA as 20% of Salary
   c) PF as 8% of Salary
   d) IT as 5% of Salary
   e) Deductions as sum of PF and IT
   f) Gross Salary as sum of Salary, HRA, DA
   g) Net Salary as Gross Salary - Deductions

Print the payslip neatly with all details */
create or alter proc sp_PaySlip @empid int
as
begin
declare  @deptid int
declare @empname varchar(20)
declare @sal float , @HRA float ,@DA float ,@Deductions float
declare @PF float , @IT float ,@GrossSalary float , @NetSalary float

select @empid=empno,@empname=ename,@sal=salary,
@deptid=deptno from emp
where empno=@empid

if(@empid is null)
begin
     print 'No Employee Found with this '+@empid +'Id'
	 return
end
else
begin
----calculate HRA of employee----
set @HRA=@sal*0.10
----calculate DA of employee----
set @DA=@sal*0.20
----calculate PF of employee----
set @PF=@sal*0.08
----calculate IT of employee----
set @IT=@sal*0.05
----calculate Deduction of  employee----
set @Deductions=@PF+@IT
----calculate Gross salary of  employee----
set @GrossSalary=@sal+@HRA+@DA
----calculate Net Salary of  employee----
set @NetSalary=@GrossSalary-@Deductions

----printing pay slip of employee---
print 'Employee Id           :' +'  '+cast(@empid as varchar)
print 'Employee Name         :' +'  '+@empname 
print 'Employee Salary       :' +'  '+cast(@sal as varchar)
print 'Employee HRA          :' +'  '+cast(@HRA as varchar)
print 'Employee DA           :' +'  '+cast(@DA as varchar)
print 'Employee PF           :' +'  '+cast(@PF as varchar)
print 'Employee IT-tax       :' +'  '+cast(@IT as varchar)
print 'Employee Deductions   :' +'  '+cast(@Deductions as varchar)
print 'Employee Gross Salary :' +'  '+cast(@GrossSalary as varchar)
print 'Employee Net Salary   :' +'  '+cast(@NetSalary as varchar)
end
end

----calling the above function and passing the employee id to print pay slip
exec sp_PaySlip @empid=7369

---------------------------------------------------------------------------------------------



/* Q2.Create a trigger to restrict data manipulation on
   EMP table during General holidays. Display the error
   message like “Due to Independence day you cannot 
   manipulate data” or "Due To Diwali", you cannot manipulate" etc

Note: Create holiday table with (holiday_date,Holiday_name). 
Store at least 4 holiday details. try to match and stop manipulation 
*/
 create table HolidayList (
 holidayDate date,
 holidayName varchar (20) )

 insert into HolidayList values
 ('2025-07-24' , 'HoliDay'),
 ('2025-03-31' , 'Eid'),
 ('2025-08-15' , 'Independence Day'),
 ('2025-10-07' , 'Diwali'),
 ('2025-10-02' , 'Gandhi Jayanti')

 select * from HolidayList


 ---create trigger to restrict data manipulation----
 create or alter trigger tg_NoManipulation
 on emp
 for insert,update, delete
 as
 begin
 declare @holiday date ;
 
 declare @holidayname varchar(40);

   select @holiday=holidaydate, @holidayname=holidayname from HolidayList
   where  holidayDate=cast(getdate() as date)
 
 if @holiday=cast(getdate() as date)
    begin
        raiserror('Due to %s, you cannot manipulate data.', 16, 1, @holidayName)
        rollback
    end
end

-----checking for inserting a data-----
insert into emp(empno,ename,job,salary,deptno) values(2345,'Shami','clerk',677899,10)
---for update---
update emp set ename ='Rahul' where empno=2024
---for deletion----
delete from emp where empno=2024







