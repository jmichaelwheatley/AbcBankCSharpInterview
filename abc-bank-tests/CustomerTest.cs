using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.TypeDefinitions;
using abc_bank.Models;
using System.Linq;
using abc_bank.Implementation;
using abc_bank.Abstractions.Classes;

namespace abc_bank_tests
{

    [TestClass]
    public class CustomerTest
    {     

        private const string DEFAULT_CUSTOMER_NAME = "Oscar";
        private Customer CreateDefaultCustomer() => new Customer(DEFAULT_CUSTOMER_NAME);


        [TestMethod]
        public void CanCreateCustomerWithSavings()
        {
            Customer newCustomer = CreateDefaultCustomer();
            newCustomer.OpenAccount(new SavingsAccount());

            Assert.AreEqual(1, newCustomer.GetNumberOfAccounts());
            Assert.AreEqual(AccountType.SAVINGS, newCustomer.AccountTypes[0]);
        }

        [TestMethod]
        public void CanCreateCustomerWithChecking()
        {
            Customer newCustomer = CreateDefaultCustomer();
            newCustomer.OpenAccount(new CheckingAccount());

            Assert.AreEqual(1, newCustomer.GetNumberOfAccounts());
            Assert.AreEqual(AccountType.CHECKING, newCustomer.AccountTypes[0]);
        }

        [TestMethod]
        public void CanCreateCustomerWithMaxi()
        {
            Customer newCustomer = CreateDefaultCustomer();
            newCustomer.OpenAccount(new MaxiSavingsAccount());

            Assert.AreEqual(1, newCustomer.GetNumberOfAccounts());
            Assert.AreEqual(AccountType.MAXI_SAVINGS, newCustomer.AccountTypes[0]);
        }

        [TestMethod]
        public void CanCreateWithTwoAccounts()
        {
            Customer newCustomer = CreateDefaultCustomer();
            newCustomer.OpenAccount(new SavingsAccount());
            newCustomer.OpenAccount(new CheckingAccount());

            Assert.AreEqual(2, newCustomer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void CanCreateWithThreeAccounts()
        {
            Customer newCustomer = CreateDefaultCustomer();
            newCustomer.OpenAccount(new SavingsAccount());
            newCustomer.OpenAccount(new CheckingAccount());
            newCustomer.OpenAccount(new MaxiSavingsAccount());

            Assert.AreEqual(3, newCustomer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void CanDepositAmount()
        {
            decimal depositAmount = 99.0m;

            Customer newCustomer = CreateDefaultCustomer();
            AccountBase checkingAccount = new CheckingAccount();
            newCustomer.OpenAccount(checkingAccount);

            Assert.AreEqual(0, checkingAccount.sumTransactions());

            checkingAccount.Deposit(depositAmount);
            Assert.AreEqual(depositAmount, checkingAccount.sumTransactions());
        }

        [TestMethod]
        public void CanWithdrawAmount()
        {
            decimal depositAmount = 99.0m;
            decimal withdrawAmount = 50.50m;

            Customer newCustomer = CreateDefaultCustomer();
            AccountBase checkingAccount = new CheckingAccount();
            newCustomer.OpenAccount(checkingAccount);

            checkingAccount.Deposit(depositAmount);
            checkingAccount.Withdraw(withdrawAmount);
            Assert.AreEqual(depositAmount - withdrawAmount, checkingAccount.sumTransactions());
        }

        [TestMethod]
        public void VerifyTextStatementContent()
        {

            Customer customer = CreateTwoAccountCustomer(100.0m, 4000.0m, 0, 200.0m);
            CustomerStatementSummary statement = customer.GetStatement();

            Assert.AreEqual($"Statement for {DEFAULT_CUSTOMER_NAME}\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", statement.SummaryAsText);
        }

        [TestMethod]
        public void StatementHasCorrectAccountTypeTotals()
        {
            CustomerStatementSummary statement = CreateTwoAccountCustomer(100.0m, 4000.0m, 0, 200.0m).GetStatement();
            decimal checkingTotal = statement.AccountSummaryData.Where(acc => acc.AccountType == AccountType.CHECKING).Single().Total;
            decimal savingsTotal = statement.AccountSummaryData.Where(acc => acc.AccountType == AccountType.SAVINGS).Single().Total;

            Assert.AreEqual(checkingTotal, 100.0m);
            Assert.AreEqual(savingsTotal, 3800.0m);
        }

        public void StatementHasValidTotal()
        {
            CustomerStatementSummary statement = CreateTwoAccountCustomer(100.0m, 4000.0m, 0, 200.0m).GetStatement();          
            Assert.AreEqual(statement.Total, 3900.0m);
        }

        private Customer CreateTwoAccountCustomer(decimal depositCheckingAmount, decimal depositSavingsAmount, decimal withdrawCheckingAmount, decimal withdrawSavingsAmount)
        {
            AccountBase checkingAccount = new CheckingAccount();
            AccountBase savingsAccount = new SavingsAccount();

            Customer newCustomer = CreateDefaultCustomer();
            newCustomer.OpenAccount(checkingAccount);
            newCustomer.OpenAccount(savingsAccount);

            if (depositCheckingAmount > 0) { checkingAccount.Deposit(depositCheckingAmount); }
            if (depositSavingsAmount > 0) { savingsAccount.Deposit(depositSavingsAmount); }
            if (withdrawCheckingAmount > 0) { checkingAccount.Withdraw(withdrawCheckingAmount); }
            if (withdrawSavingsAmount > 0) { savingsAccount.Withdraw(withdrawSavingsAmount); }

            return newCustomer;
        }
    }
}
