using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Exception Handling :  
1.
•	You have a class which has methods for transaction for a banking system. (created earlier)
•	Define your own methods for deposit money, withdraw money and balance in the account.
•	Write your own new application Exception class called InsuffientBalanceException. 
•	This new Exception will be thrown in case of withdrawal of money from the account where customer doesn’t have sufficient balance.
•	Identify and categorize all possible checked and unchecked exception.
 */
namespace Assignment5
{
    class Account : Exception
    {
        public int  AccountNO;
        public string CustomerName;
        public string AccType;


        public static int balance;

        public Account(int accNo, string custName, string accType)
        {
            this.AccountNO = checked (accNo);
            this.CustomerName = custName;
            this.AccType = accType;

        }
        public void Transaction()
        {
            Console.WriteLine(" Select your transaction : ");
            Console.WriteLine("D-> Deposit and W->Withdrawl");

        }

        public int credit(int amount)

        {
            if (balance >= amount)
            {

                balance = balance - amount;
                Console.WriteLine("Transaction Successfull!");
                Console.Write("Available Balance : ");
                return balance;
            }

            else
            {
                throw new InsuffientBalanceException("Insufficient Balance!");
            }

        }

            public int Debit(int amount)

            {

                balance = balance + amount;
                Console.WriteLine("Transaction Successfull!");
                Console.Write("Available Balance : ");
                return balance;



            }
            public void showData()
            {
                Console.WriteLine("Account Number : " + unchecked (AccountNO));
                Console.WriteLine("Customer Name : " + CustomerName);
                Console.WriteLine("Account Type=" + AccType);
                Console.WriteLine("Available Balance : " + balance);

            }
        
    }

    class Assignment5Q1: Exception
    {
         

        static void Main(string[] args)
        {
            int AccountNO;
            string CustomerName;
            string AccType;
            
            

            Console.WriteLine("Enter the account number:");
            AccountNO=Convert.ToInt32( Console.ReadLine());
            Console.WriteLine("Enter the customer name:");
            CustomerName = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the account Type:");
            AccType = Convert.ToString(Console.ReadLine());

            Account customer = new Account(AccountNO, CustomerName, AccType);
            Console.WriteLine("Enter the initial balance :");
            Account.balance = Convert.ToInt32(Console.ReadLine());
           
            customer.showData();
            int amount;
            try
            {
                customer.Transaction();
                string type = Convert.ToString(Console.ReadLine());
                switch (type)
                {
                    case "D":
                        Console.WriteLine("Enter the amount to Deposit  :");
                         amount = Convert.ToInt32(Console.ReadLine());
                        customer.Debit(amount);
                         
                        break;
                    case "W":
                        Console.WriteLine("Enter the amount to withdrawl  :");
                        amount = Convert.ToInt32(Console.ReadLine());
                        customer.credit(amount);
                       

                        break;
                    default:
                        Console.WriteLine("Invalid Choice !");
                        break;
                }
            }
            catch(InsuffientBalanceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Thank you for banking with us !");
            }
            Console.Read();
        }
    }

}

