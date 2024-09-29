using System;

namespace abc_bank
{
    public class Transaction
    {
        public readonly double _amount;
        public double Amount => _amount;

        private DateTime transactionDate;

        public Transaction(double amount) 
        {
            _amount = amount;
            transactionDate = DateProvider.getInstance().Now();
        }
    }
}
