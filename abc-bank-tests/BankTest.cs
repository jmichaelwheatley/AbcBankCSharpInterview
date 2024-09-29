using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.TypeDefinitions;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        private const string DEFAULT_CUSTOMER_NAME = "John";
        private Customer CreateDefaultCustomer() => new Customer(DEFAULT_CUSTOMER_NAME);

        [TestMethod]
        public void CustomerSummary()
        {
            Bank bank = new Bank();
            Customer customer = CreateDefaultCustomer();
            customer.OpenAccount(new Account(AccountType.CHECKING));
            bank.AddCustomer(customer);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccountInterest()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.CHECKING);      
            bank.AddCustomer(CreateDefaultCustomer().OpenAccount(checkingAccount));
            
            checkingAccount.Deposit(100.0m);
            Assert.AreEqual(0.1m, bank.totalInterestPaid());
        }

        [TestMethod]
        public void SavingsAccountInterest()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.SAVINGS);
            bank.AddCustomer(CreateDefaultCustomer().OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0m);

            Assert.AreEqual(2.0m, bank.totalInterestPaid());
        }

        [TestMethod]
        public void MaxiSavingsAccountInterest()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.MAXI_SAVINGS);
            bank.AddCustomer(CreateDefaultCustomer().OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0m);

            Assert.AreEqual(170.0m, bank.totalInterestPaid());
        }
    }
}
