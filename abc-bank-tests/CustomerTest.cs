using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.TypeDefinitions;
using abc_bank.Models;
using System.Linq;
using abc_bank.Implementation;
using abc_bank.Abstractions.Classes;
using abc_bank.Other;
using abc_bank.Abstractions.Interfaces;
using System.Collections.Generic;
using abc_bank.Models.Results;

namespace abc_bank_tests
{

    [TestClass]
    public class CustomerTest
    {
        private const string DEFAULT_CUSTOMER_NAME = "Oscar";
        private const string DEFAULT_CUSTOMER_LAST_NAME = "Whimsy";

        private MyFakeResolver _resolver;
        private ICustomerCreationService _customerCreationService;

        private Customer CreateDefaultCustomer() => new Customer(DEFAULT_CUSTOMER_NAME);

        [TestInitialize]
        public void InitTests()
        {
            _resolver = new MyFakeResolver();
            _customerCreationService = _resolver.ResolveFor<ICustomerCreationService>();
        }

        [TestMethod]
        public void CanCreateCustomerWithSavings()
        {
            CreateCustomerData cd = new CreateCustomerData(DEFAULT_CUSTOMER_NAME, DEFAULT_CUSTOMER_LAST_NAME,
                new List<CreateCustomerInitialAccountData>() { new CreateCustomerInitialAccountData(AccountType.SAVINGS, 0) });

            Customer newCustomer = _customerCreationService.CreateCustomer(cd).Customer;

            Assert.AreEqual(1, newCustomer.GetNumberOfAccounts());
            Assert.AreEqual(AccountType.SAVINGS, newCustomer.AccountTypes[0]);
        }

        [TestMethod]
        public void CanCreateCustomerWithChecking()
        {
            CreateCustomerData cd = new CreateCustomerData(DEFAULT_CUSTOMER_NAME, DEFAULT_CUSTOMER_LAST_NAME,
                new List<CreateCustomerInitialAccountData>() { new CreateCustomerInitialAccountData(AccountType.CHECKING, 0) });

            Customer newCustomer = _customerCreationService.CreateCustomer(cd).Customer;

            Assert.AreEqual(1, newCustomer.GetNumberOfAccounts());
            Assert.AreEqual(AccountType.CHECKING, newCustomer.AccountTypes[0]);
        }

        [TestMethod]
        public void CanCreateCustomerWithMaxi()
        {
            CreateCustomerData cd = new CreateCustomerData(DEFAULT_CUSTOMER_NAME, DEFAULT_CUSTOMER_LAST_NAME,
                new List<CreateCustomerInitialAccountData>() { new CreateCustomerInitialAccountData(AccountType.MAXI_SAVINGS, 0) });

            Customer newCustomer = _customerCreationService.CreateCustomer(cd).Customer;

            Assert.AreEqual(1, newCustomer.GetNumberOfAccounts());
            Assert.AreEqual(AccountType.MAXI_SAVINGS, newCustomer.AccountTypes[0]);
        }

        [TestMethod]
        public void CanCreateWithTwoAccounts()
        {
            CreateCustomerData cd = new CreateCustomerData(DEFAULT_CUSTOMER_NAME, DEFAULT_CUSTOMER_LAST_NAME,
               new List<CreateCustomerInitialAccountData>() {
                                                                new CreateCustomerInitialAccountData(AccountType.SAVINGS, 0),
                                                                new CreateCustomerInitialAccountData(AccountType.CHECKING, 0) });

            Customer newCustomer = _customerCreationService.CreateCustomer(cd).Customer;
            Assert.AreEqual(2, newCustomer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void CanCreateWithThreeAccounts()
        {
            CreateCustomerData cd = new CreateCustomerData(DEFAULT_CUSTOMER_NAME, DEFAULT_CUSTOMER_LAST_NAME,
                new List<CreateCustomerInitialAccountData>() {
                                                                new CreateCustomerInitialAccountData(AccountType.SAVINGS, 0),
                                                                new CreateCustomerInitialAccountData(AccountType.CHECKING, 0),
                                                                new CreateCustomerInitialAccountData(AccountType.MAXI_SAVINGS, 0) });

            Customer newCustomer = _customerCreationService.CreateCustomer(cd).Customer;

            Assert.AreEqual(3, newCustomer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void CanDepositAmountOnCustomerCreation()
        {
            decimal depositAmount = 99.0m;

            CreateCustomerData cd = new CreateCustomerData(DEFAULT_CUSTOMER_NAME, DEFAULT_CUSTOMER_LAST_NAME,
                   new List<CreateCustomerInitialAccountData>() { new CreateCustomerInitialAccountData(AccountType.CHECKING, depositAmount) });

            CreateCustomerResult result = _customerCreationService.CreateCustomer(cd);            
            Assert.AreEqual(depositAmount, result.Accounts[0].sumTransactions());
        }

        [TestMethod]
        public void CanDepositAmountAfterCustomerCreation()
        {
            decimal depositAmount = 99.0m;

            CreateCustomerData cd = new CreateCustomerData(DEFAULT_CUSTOMER_NAME, DEFAULT_CUSTOMER_LAST_NAME,
                   new List<CreateCustomerInitialAccountData>() { new CreateCustomerInitialAccountData(AccountType.CHECKING, 0) });

            CreateCustomerResult result = _customerCreationService.CreateCustomer(cd);
            CheckingAccount checkingAccount = result.Accounts[0] as CheckingAccount;

            Assert.AreEqual(0, checkingAccount.sumTransactions());

            checkingAccount.Deposit(depositAmount);
            Assert.AreEqual(depositAmount, checkingAccount.sumTransactions());
        }

        [TestMethod]
        public void CanWithdrawAmount()
        {
            decimal depositAmount = 99.0m;
            decimal withdrawAmount = 50.50m;

            CreateCustomerData cd = new CreateCustomerData(DEFAULT_CUSTOMER_NAME, DEFAULT_CUSTOMER_LAST_NAME,
                    new List<CreateCustomerInitialAccountData>() { new CreateCustomerInitialAccountData(AccountType.CHECKING, 0) });

            CreateCustomerResult result = _customerCreationService.CreateCustomer(cd);
            CheckingAccount checkingAccount = result.Accounts[0] as CheckingAccount;

            checkingAccount.Deposit(depositAmount);
            checkingAccount.Withdraw(withdrawAmount);
            Assert.AreEqual(depositAmount - withdrawAmount, checkingAccount.sumTransactions());
        }

        [TestMethod]
        public void VerifyTextStatementContent()
        {

            Customer customer = CreateTwoAccountCustomer(100.0m, 4000.0m, 0, 200.0m);
            CustomerStatementSummary statement = customer.GetStatement();

            Assert.AreEqual($"Statement for {DEFAULT_CUSTOMER_NAME} {DEFAULT_CUSTOMER_LAST_NAME}\n" +
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
            CreateCustomerData cd = new CreateCustomerData(DEFAULT_CUSTOMER_NAME, DEFAULT_CUSTOMER_LAST_NAME,
                    new List<CreateCustomerInitialAccountData>() { 
                        new CreateCustomerInitialAccountData(AccountType.CHECKING, depositCheckingAmount), 
                        new CreateCustomerInitialAccountData(AccountType.SAVINGS, depositSavingsAmount) });

            CreateCustomerResult result = _customerCreationService.CreateCustomer(cd);
            AccountBase checkingAccount = result.Accounts.Where(a => a.GetAccountType == AccountType.CHECKING).Single();
            AccountBase savingsAccount = result.Accounts.Where(a => a.GetAccountType == AccountType.SAVINGS).Single();

            if (withdrawCheckingAmount > 0) { checkingAccount.Withdraw(withdrawCheckingAmount); }
            if (withdrawSavingsAmount > 0) { savingsAccount.Withdraw(withdrawSavingsAmount); }

            return result.Customer;
        }

    }
}
