use Assignments

------Assignment 3---------
--------Q1.retrive list of managers-----
select * from EMP
where job ='Manager'

---Q2.--employees earning more than 1000/month---
select ename , salary from emp 
where salary > 1000
---Q3.employees name and salary except james----
select * from emp 
where ename not in(select ename from  emp where ename='JAMES')
--Q4.Employees list whose name begins with 's'
select * from EMP
where ename like 's%'
--q5.Emplyees name that contains a---
select * from emp
where ename like '%A%'
--q6.Emplyees name that contains L as third character---
select * from emp
where ename like '__[L]%'
---Q7.daily salary of JONES---
select ename , (salary/30) as 'Daily Salary'
from emp where ename='JONES'
----Q8-Total monthly salary of all employees
select   sum(salary ) as 'Total Monthly Salary'
from EMP
--Q9.Print the average annual salary-----
select avg(salary) as 'Annual Salary'
from emp 
--Q.10. Select the name, job, salary, department number of all employees except 
--SALESMAN from department number 30. 
Select  ename, job, salary, deptno from emp 
where job not in(select job from emp where job='SALESMAN')
---Q.11. List unique departments of the EMP table. 
select distinct e1.deptno ,d1.dname  from emp e1 , DEPT d1
where e1.deptno=d1.deptno
--Q12. List the name and salary of employees who earn more than 1500 and are in department 10 or 30. 
--Label the columns Employee and Monthly Salary respectively.
select ename as 'Employee Name', salary as 'Monthly Salary' 
from emp where salary > 1500 and deptno in (10, 30)
--Q13. Display the name, job, and salary of all the employees whose job is MANAGER or ANALYST
--and their salary is not equal to 1000, 3000, or 5000. 
select ename, job, salary from emp where job in 
('manager', 'analyst') and salary not in (1000,3000,5000)
--Q14. Display the name, salary and commission for all employees whose commission 
--amount is greater than their salary increased by 10%. 
select ename, salary, comm from emp where comm > (salary + (salary * 0.1))
--Q15. Display the name of all employees who have two Ls in their name and 
---are in department 30 or their manager is 7782. 
select ename from emp where ename like ('%l%l%') and deptno = 30 or mgr_id = 7782
---Q16.Display the names of employees with experience of over 30 years and 
--under 40 yrs. Count the total number of employees. 

select count(*) as 'Total Experienced' 
 from emp  where datediff(year, hire_date, getdate())  between 31 and 39
  

--Q17.Retrieve the names of departments in ascending order and their employees in 
--descending order. 
select d1.dname, e1.ename from dept d1 , emp e1
where e1.deptno = d1.deptno 
order by d1.dname , e1.ename desc 
--18. Find out experience of MILLER. 
select ename, datediff(year, hire_date, getdate()) 
as'Experience in Year' from emp where ename = 'MILLER'
 