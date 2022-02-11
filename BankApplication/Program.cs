using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Cis 297 Assignment 3 
/// Ahmed Mazloum
/// </summary>

namespace BankApplication
{
    // Processing Accounts polymorphically.
    using System;
    /// <summary>
    /// Test class
    /// </summary>
    public class AccountTest
    {
        /// <summary>
        /// Main class that runs the test
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // create array of accounts
            Account[] accounts = new Account[4];

            // initialize array with Accounts
            accounts[0] = new SavingsAccount(25M, .03M);
            accounts[1] = new CheckingAccount(80M, 1M);
            accounts[2] = new SavingsAccount(200M, .015M);
            accounts[3] = new CheckingAccount(400M, .5M);

            // loop through array, prompting user for debit and credit amounts
            for (int i = 0; i < accounts.Length; i++)
            {
                Console.WriteLine($"Account {i + 1} balance: {accounts[i].Balance:C}");

                Console.Write($"\nEnter an amount to withdraw from Account {i + 1}: ");
                decimal withdrawalAmount = decimal.Parse(Console.ReadLine());

                accounts[i].Debit(withdrawalAmount); // attempt to debit

                Console.Write($"\nEnter an amount to deposit into Account {i + 1}: ");
                decimal depositAmount = decimal.Parse(Console.ReadLine());

                // credit amount to Account
                accounts[i].Credit(depositAmount);

                // if Account is a SavingsAccount, calculate and add interest
                if (accounts[i] is SavingsAccount)
                {
                    // downcast
                    SavingsAccount currentAccount = (SavingsAccount)accounts[i];

                    decimal interestEarned = currentAccount.CalculateInterest();
                    Console.WriteLine($"Adding {interestEarned:C} interest to Account {i + 1} (a SavingsAccount)");
                    currentAccount.Credit(interestEarned);
                }

                Console.WriteLine($"\nUpdated Account {i + 1} balance: {accounts[i].Balance:C}\n\n");
            }
        }
    }
    /// <summary>
    /// Checking Account subtracts the fee after each transaction
    /// </summary>
    public class CheckingAccount:Account {

        private decimal _fee;
        public CheckingAccount(decimal balance, decimal fee) : base(balance) {
            Fee = fee;
        
        }

        public decimal Fee {
            get {
                return _fee;
            }
            set {
                if (value > 0)
                    _fee = value;
                else
                    throw new Exception("Fee cannot be less than zero");
            }
        
        }
        public override void Credit(decimal amount)
        {
            base.Credit(amount);
            Balance -= Fee;
        }
        public override bool Debit(decimal amount)
        {
            if (base.Debit(amount)) {
                Balance -= Fee;
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// Adds insterset rate to total amout of money
    /// </summary>
    public class SavingsAccount : Account
    {
        private decimal _interestRate;

        public SavingsAccount(decimal balance, decimal interestRate) : base(balance) {

            IntersetRate = interestRate;
        }
        public decimal CalculateInterest() {

            return IntersetRate * base.Balance;
        }

        public decimal IntersetRate
        {
            get {
                return _interestRate;
            }
            set {
                if (value > 0)
                    _interestRate = value;
                else
                    throw new Exception("Interest rate must be greater than zero");
            
            }
        
        }

    }
    /// <summary>
    /// Adds and subtracts amount from balance
    /// </summary>
    public class Account
    {

        private decimal _balance;
        public Account(decimal balance) {
            Balance = balance;
        
        }
        public decimal Balance {

            get {
                return _balance;
            
            }

            set {
                if (value >= 0)
                    _balance = value;
                else
                    throw new Exception("Balance cannot be negative");

            }
        }
        public virtual void Credit(decimal amount) {
            if (amount > 0)
                Balance += amount;
            else
                throw new Exception("Credited amount must be greater thean zero");
        }

        public virtual bool Debit(decimal amount) {
            bool OK = true;
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                OK = true;
            }
            else {
                Console.Write("Debit amount execeded amount balance");
                OK = false;
            }
            return OK;
        }
    }
}

