using System;
using System.Collections.Generic;

namespace abc_bank.Models
{    
    public class CustomerStatementSummary
    {
        public readonly List<AccountTransactionInfo> AccountSummaryData;
        public readonly string SummaryAsText;
        public readonly double Total;
        public double NumberOfAccounts => AccountSummaryData.Count;

        public CustomerStatementSummary(List<AccountTransactionInfo> accountSummaryData, string summaryAsText, double total)
        {
            AccountSummaryData = accountSummaryData;
            SummaryAsText = summaryAsText;
            Total = total;
        }
    }
}
