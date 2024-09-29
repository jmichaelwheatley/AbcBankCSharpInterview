using System;
using System.Collections.Generic;

namespace abc_bank.Models
{    
    public class CustomerStatementSummary
    {
        public readonly List<AccountTransactionInfo> AccountSummaryData;
        public readonly string SummaryAsText;
        public readonly decimal Total;
        public decimal NumberOfAccounts => AccountSummaryData.Count;

        public CustomerStatementSummary(List<AccountTransactionInfo> accountSummaryData, string summaryAsText, decimal total)
        {
            AccountSummaryData = accountSummaryData;
            SummaryAsText = summaryAsText;
            Total = total;
        }
    }
}
