using System;


namespace Assignment3
{
    class Account
    {
        public int AccountNO;
        public string CustomerName;
        public string AccType;
        public string TransactionType;
        public int amount;
        public int balance;
        
        public Account(int accNo, string custName, string accType)
        {
            this.AccountNO = accNo;
            this.CustomerName = custName;
            this.AccType = accType;

        }
        public void Transaction()
        {
            Console.WriteLine(" Select your transaction : ");
            Console.WriteLine("D-> Deposit and W->Withdrawl");
            char type = Convert.ToChar(Console.Read());
            switch (type) {
                case 'D':
                    
                    Console.WriteLine(credit(amount));
                    break;
                case 'W':
                    Console.WriteLine(Debit(amount));
  
                    break;
                default:
                    Console.WriteLine("Invalid Choice !");
                    break;
            }
        }

        public int credit(int amount)

        {
            this.amount = amount;
            Console.WriteLine("Enter the amount to deposit :");
            amount = Convert.ToInt32(Console.ReadLine());
            balance = balance - amount;
            Console.WriteLine("Transaction Successfull!");
            Console.Write("Available Balance : ");
            return balance;

            //return balance
        }

        public int Debit(int amount)

        {
            this.amount = amount;
            Console.WriteLine("Enter the amount to withdrawl  :");
            amount = Convert.ToInt32(Console.ReadLine());
        
            balance = balance + amount;
            Console.WriteLine("Transaction Successfull!");
            Console.Write("Available Balance : ");
            return balance;

        }
        public void showData()
        {
            Console.WriteLine("Account Number : "+AccountNO);
            Console.WriteLine("Customer Name : " + CustomerName);
            Console.WriteLine("Account Type=" + AccType);
            Console.WriteLine("Available Balance : " + balance);
        
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the account number:");
            
            Account customer = new Account(45678921,"Shami Gaffar","Saving");
            customer.balance = 450000;
            customer.showData();
            customer.Transaction();
            Console.Read();
        }
    }
}
