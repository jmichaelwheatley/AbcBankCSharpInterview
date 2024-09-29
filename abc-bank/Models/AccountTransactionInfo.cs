using System;
using abc_bank.TypeDefinitions;


namespace abc_bank.Models
{
    public class AccountTransactionInfo
    {
        public readonly AccountType AccountType;
        public readonly decimal Total;
        public readonly decimal[] TransactionAmounts;

        public AccountTransactionInfo(AccountType accountType, decimal total, decimal[] transactionAmounts)
        {
            AccountType = accountType;
            Total = total;
            TransactionAmounts = transactionAmounts;
        }
    }
}
