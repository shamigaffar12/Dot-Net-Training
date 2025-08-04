use CodeChallenge

---Code Challenge 5---

----1.Write a query to display your birthday( day of week)

declare @dob date
Select @dob='2004-02-25'
select Datename(WEEKDAY ,@dob) as ' birthday'



---Q2.	Write a query to display your age in days
declare @birthdate date = '2025-02-25';

;with cte_dob as (
    select @birthdate as birthdate
)
select
    birthdate,
    datediff(day, birthdate, cast(getdate() as date)) as age_in_days
from cte_dob;



------Q3.Write a query to display all employees information
----those who joined before 5 years in the current month
select * from assignments.dbo.EMP;


select *from assignments.dbo.emp
where year(hire_date) = year(getdate()) - 5
    and month(hire_date) = month(getdate());
/*Q4.Create table Employee with empno, ename, sal, doj columns or use your 
 emp table and perform the following operations in a single transaction
	a. First insert 3 rows 
	b. Update the second row sal with 15% increment  
    c. Delete first row.
After completing above all actions, recall the deleted 
row without losing increment of second row.*/

begin transaction

-- insert
insert into assignments.dbo.emp (empno ,ename ,job,salary,hire_date) values
                               (2023, 'Shami','Enginer', 67890, '2009-10-12'),
                               (2024, 'Rahul','analyst' ,5678, '2010-02-09'),
                               (2025, 'Akhilesh','researcher', 55000, '2021-12-20')

-- b. update the second row's salary 
update Assignments.dbo.EMP
set salary = salary * 1.15
where empno = 2024

-- c. delete the first row
delete from assignments.dbo.emp where empno = 2023;

save transaction t1

commit

-- recall the deleted row without losing the increment of the second row:

rollback transaction t1

-- commit the whole transaction
commit;

/*--Q5.Create a user defined function calculate 
      Bonus for all employees of a  given dept using following conditions
	a.For Deptno 10 employees 15% of sal as bonus.
	b.For Deptno 20 employees  20% of sal as bonus
	c For Others employees 5%of sal as bonus
	*/

	
	create  function fn_CalculateBonus(
	 @deptno numeric ,@sal float)

	returns float
	as 
	begin
	declare @bonus float
	if(@deptno = 10)
	begin
		set @bonus=@sal*0.20
	
	end
	if @deptno=20
	begin
	set @bonus=@sal*0.20
	end

	else 
	begin
	set @bonus=@sal*0.5
	end
	return @bonus
	end

	
select empno,deptno,salary ,dbo.fn_CalculateBonus(deptno, sal) as bonus
from dbo.assignments.dbo.emp;

---Q6.6. Create a procedure to update the salary of employee by 500 whose 
--dept name is Sales and current salary is below 1500 (use emp table)

create procedure updateSal
as
begin
    update assignments.dbo.emp
    set e.salary = e.salary + 500
    from asssignments.dbo.emp e
    join assignments.dbo.dept d on e.deptno = d.deptno
    where d.dname = 'sales'
      and e.salary < 1500;
end;
 
 exec asssignments.dbo.emp.updateSal

 SELECT * FROM sys.servers WHERE name = 'ICS-LT-4V2CJ84\SQLEXPRESS';

 EXEC sp_testlinkedserver 'ICS-LT-4V2CJ84\SQLEXPRESS';
