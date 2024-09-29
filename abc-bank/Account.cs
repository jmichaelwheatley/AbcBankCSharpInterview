using System;
using System.Collections.Generic;
using abc_bank.TypeDefinitions;

namespace abc_bank
{
    public class Account
    {   

        private readonly AccountType _accountType;

        public AccountType GetAccountType => _accountType;
        public List<Transaction> transactions;

        public Account(AccountType accountType) 
        {
            _accountType = accountType;
            transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public decimal InterestEarned() 
        {
            decimal amount = sumTransactions();
            switch(_accountType){
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001m;
                    else
                        return 1 + (amount-1000) * 0.002m;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case AccountType.MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02m;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05m;
                    return 70 + (amount-2000) * 0.1m;
                default:
                    return amount * 0.001m;
            }
        }

        public decimal sumTransactions() {
            decimal amount = 0.0m;
            foreach (Transaction t in transactions)
                amount += t.Amount;
            return amount;
        }
    }
}
