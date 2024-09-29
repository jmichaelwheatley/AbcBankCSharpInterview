using System;
using System.Collections.Generic;
using System.Linq;
using abc_bank.TypeDefinitions;

namespace abc_bank.Abstractions.Classes
{
    public abstract class AccountBase
    {
        public abstract AccountType GetAccountType { get; }
        public List<Transaction> transactions;

        public AccountBase()
        {            
            transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount));
            }
        }

        public virtual decimal InterestEarned()
        {
            decimal amount = sumTransactions();
            return amount * 0.001m;            
        }

        public decimal sumTransactions()
        {
            decimal amount = 0.0m;
            foreach (Transaction t in transactions)
                amount += t.Amount;
            return amount;
        }
    }
}
