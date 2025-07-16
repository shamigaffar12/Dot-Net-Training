use CodeChallenge

/*Create a book table with id as primary key 
and provide the appropriate data type to
other attributes
.isbn no should be unique for each book */

/*create table books ( id int primary key ,
                     title varchar(50) ,
					 author varchar(45),
					 isbn numeric unique,
					 published_date DateTime)

--Insert data into table---
Insert into  books values(1,'My First SQL book' ,'Mary Parker' ,981483029127,'2012-02-22 12:08:17' ) ,
(2,'My Second SQL book' ,'John Mayer' ,857300923713,'1972-07-03 09:22:45' ) ,
(3,'MyThird SQL book' ,'Cary FLint' ,523120967812,'2015-10-18 14:05:44' ) */

 --query1--Write a query to fetch the details of the books written by author whose name ends with er. 

 Select* from dbo.books where author like '%er'



 --query2
 /*
 create table reviews (
    id int not null ,    
    book_id int primary key ,                
    reviewer_name varchar (50) ,
    content varchar(10) ,   
    rating int,
    published_date DateTime ,         
    constraint Reviews_Books foreign key (id)
        references dbo.books(id)
       ) */
	   
	   ---query
	  -- Display the Title ,Author and ReviewerName for all the books from the above table  
	  select b.Title ,b.Author ,r.reviewer_name from books b , reviews r where b.id=r.id
	  --query2
	 select reviewer_name, count(distinct id) AS books_review 
	 FROM reviews group BY reviewer_name having
     count (distinct id) >1


/*	 --q3--
	 create table customer (
    id int primary key ,
    name varchar (50) not null,
    age int  ,
    address varchar(90),
    salary decimal(10, 2) 
) */

--query q3
select name
from customer
where address like '%o%'

---q4

/*create table orders (
    oid int primary key ,
    DATE datetime,
    customer_id int not null,
    amount decimal(10,2) 
    foreign key (customer_id) references customer(id)
)*/

---query  

--Write a query to display the   Date,Total no of customer  placed order on same Date
select date, count(distinct customer_id) as total_customers
from orders
group by Date
 ----

 /* create table Employee (
    id int primary key ,
    name varchar (50) not null,
    age int  ,
    address varchar(90),
    salary decimal(10, 2) 
) */

--query
select name , lower(name) ' employename'
from Employee
where salary is null
----

create table studentDetails (
    registernumber int primary key,
    name varchar(100) ,
    age int ,
    qualification varchar(100),
    mobileno varchar(15) unique,
    mailid varchar(100),
    location varchar(100),
    gender varchar(10)
)

---query Write a sql server query to display the Gender,Total no of male and female from the above  relation 

select gender, count(*) as total from studentDetails
group by  gender





 

