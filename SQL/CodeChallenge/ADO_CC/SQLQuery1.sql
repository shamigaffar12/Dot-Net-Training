use CodeChallenge

create table EmployeeDetails (
    EmpId    int identity(1,1) primary key,
    Name     varchar(25),
    Salary   float,
    Gender   varchar(10))


	select * from EmployeeDetails

	create  procedure getEmpDetail @eName varchar ,
	 @eSalary float ,@eGender varchar 
	 as
     begin
         insert into EmployeeDetails(name, salary, gender)
          values ( @eName,  @eSalary ,@eGender);
		  declare @EmpId int
		  declare @NetSalary float
          select @EmpId = scope_identity() from EmployeeDetails where EmpId=@EmpId
          select @NetSalary = @eSalary-(@eSalary * 0.90) from EmployeeDetails where EmpId=@EmpId
		  select EmpId ,Name,Salary,Gender from EmployeeDetails where EmpId=@EmpId;
		  return @EmpId
end

