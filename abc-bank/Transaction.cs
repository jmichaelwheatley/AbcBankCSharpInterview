using System;

namespace abc_bank
{
    public class Transaction
    {
        public readonly decimal _amount;
        public decimal Amount => _amount;

        private DateTime transactionDate;

        public Transaction(decimal amount) 
        {
            _amount = amount;
            transactionDate = DateProvider.getInstance().Now();
        }
    }
}
