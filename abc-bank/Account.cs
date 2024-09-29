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

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned() 
        {
            double amount = sumTransactions();
            switch(_accountType){
                case AccountType.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case AccountType.MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05;
                    return 70 + (amount-2000) * 0.1;
                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions() {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.Amount;
            return amount;
        }
    }
}
