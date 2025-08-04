using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
/*
 * 1. Write a stored Procedure that inserts records in the Employee_Details table
 
The procedure should generate the EmpId automatically to insert and should return the generated value to the user
 
Also the Salary Column is a calculated column (Salary is givenSalary - 10%)
 
Table : Employee_Details (Empid, Name, Salary, Gender)
Hint(User should not give the EmpId)
 
Test the Procedure using ADO classes and show the generated Empid and Salary
 */
namespace ADO_CC
{
    class ADO_Connected
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlDataReader dr;

        static SqlConnection getConnection()
        {
            con = new SqlConnection("Data Source=ICS-LT-4V2CJ84\\SQLEXPRESS;Database=CodeChallenge;User Id =sa ; password=Sh@7991138912");
            con.Open();
            return con;
        }

        static void getEmployeeDetails()
        {
            try
            {
                con = getConnection();
                Console.WriteLine("EnterEMp DEtails  Name , Gender , Salary");
                string Name = Console.ReadLine();
                string Gender = Console.ReadLine();
                float  Salary = float.Parse(Console.ReadLine());
                SqlCommand cmd = new SqlCommand("getEmpDetail", con);

                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.AddWithValue("@eName", Name);
                cmd.Parameters.AddWithValue("@eSalary", Salary);
                cmd.Parameters.AddWithValue("@eGender", Gender);



                SqlParameter paramId = new SqlParameter("@eEmpId", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                cmd.Parameters.Add(paramId);
            
                
                
              SqlParameter paramName = new SqlParameter("@eName", SqlDbType.VarChar) { Direction = ParameterDirection.Output} {paramName.Value = 34;
                cmd.Parameters.Add(paramName);
                SqlParameter paramGender = new SqlParameter("@eGender", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output };
                
                cmd.Parameters.Add(paramGender);
                SqlParameter paramSalNet = new SqlParameter("@eNetSalary", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output };

                cmd.Parameters.Add(paramSalNet);

                int result = cmd.ExecuteNonQuery();
                if (result > 0) { 
                    Console.WriteLine("Record Inserted successfully..");
                    Console.WriteLine("Generated EmpId: " + paramId.Value);
                    Console.WriteLine("Net Salary  " + paramSalNet.Value);
            }
                else
                    Console.WriteLine("Could not Insert...");

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        static void Main(string[] args)
        {
            getEmployeeDetails();
            Console.Read();
        }

    }
   
}
