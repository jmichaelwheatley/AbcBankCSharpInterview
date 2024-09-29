using System;
using System.Collections.Generic;
using System.Linq;
using abc_bank.Abstractions.Classes;
using abc_bank.Models;
using abc_bank.TypeDefinitions;

namespace abc_bank
{
    public class Customer
    {
        private string name;
        private List<AccountBase> _accounts;

        public AccountType[] AccountTypes => _accounts.Select(a => a.GetAccountType).ToArray();

        public Customer(string name)
        {
            this.name = name;
            _accounts = new List<AccountBase>();
        }

        public string GetName()
        {
            return name;
        }

        public Customer OpenAccount(AccountBase account)
        {
            _accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return _accounts.Count;
        }

        public decimal TotalInterestEarned()
        {
            decimal total = 0;
            foreach (AccountBase a in _accounts)
                total += a.InterestEarned();
            return total;
        }



        public CustomerStatementSummary GetStatement()
        {
            List<AccountTransactionInfo> accountData = new List<AccountTransactionInfo>();
            foreach (AccountBase acc in _accounts)
            {
                accountData.Add(new AccountTransactionInfo(acc.GetAccountType, acc.sumTransactions(), acc.transactions.Select(t => t.Amount).ToArray()));
            }

            CustomerStatementSummary statement = new CustomerStatementSummary(accountData, GetStatementText(), TotalAllAccounts());
            return statement;
        }

        private string statementForAccount(AccountBase a)
        {
            string s = "";

            //Translate to pretty account type
            switch (a.GetAccountType)
            {
                case AccountType.CHECKING:
                    s += "Checking Account\n";
                    break;
                case AccountType.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case AccountType.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            decimal total = 0.0m;
            foreach (Transaction t in a.transactions)
            {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private string GetStatementText()
        {
            string statement = null;

            statement = "Statement for " + name + "\n";
            foreach (AccountBase a in _accounts) { statement += "\n" + statementForAccount(a) + "\n"; }
            statement += "\nTotal In All Accounts " + ToDollars(TotalAllAccounts());

            return statement;
        }

        private string ToDollars(decimal d)
        {
            return $"{Math.Abs(d):C2}";
        }

        private decimal TotalAllAccounts()
        {
            decimal total = 0.0m;
            foreach (AccountBase a in _accounts) { total += a.sumTransactions(); }

            return total;
        }
    }
}
