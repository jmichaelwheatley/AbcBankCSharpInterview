using System;
using abc_bank.TypeDefinitions;


namespace abc_bank.Models
{
    public class AccountTransactionInfo
    {
        public readonly AccountType AccountType;
        public readonly double Total;
        public readonly double[] TransactionAmounts;

        public AccountTransactionInfo(AccountType accountType, double total, double[] transactionAmounts)
        {
            AccountType = accountType;
            Total = total;
            TransactionAmounts = transactionAmounts;
        }
    }
}
