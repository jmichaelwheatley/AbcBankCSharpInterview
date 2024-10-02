using System;
using System.Collections.Generic;
using System.Linq;
using abc_bank.Abstractions.Classes;
using abc_bank.Abstractions.Interfaces;
using abc_bank.Models;
using abc_bank.Models.Results;
using abc_bank.TypeDefinitions;

namespace abc_bank.Implementation.Services
{
    public class CustomerCreationService : ICustomerCreationService
    {
        public CreateCustomerResult CreateCustomer(CreateCustomerData customerData)
        {
            Customer newCustomer = new Customer($"{customerData.FirstName} {customerData.LastName}");
            List<AccountBase> accounts = new List<AccountBase>();

            foreach(CreateCustomerInitialAccountData aData in customerData.Accounts)
            {
                AccountBase account = CreateNewAccount(aData.AccountType);
                newCustomer.OpenAccount(account);
                if(aData.DepositAmount > 0) { account.Deposit(aData.DepositAmount); }

                accounts.Add(account);
            }

            return new CreateCustomerResult(newCustomer, accounts.ToArray());
        }

        private AccountBase CreateNewAccount(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.CHECKING: return new CheckingAccount();
                case AccountType.SAVINGS: return new SavingsAccount();
                case AccountType.MAXI_SAVINGS: return new MaxiSavingsAccount();
                default: throw new Exception($"An unsupported AccountType value of {accountType} was given");
            }
        }
    }
}
