using System;
using System.Linq;
using System.Collections.Generic;
using abc_bank.Abstractions.Classes;
using abc_bank.TypeDefinitions;

namespace abc_bank.Implementation
{
    public class MaxiSavingsAccount : AccountBase
    {
        public override AccountType GetAccountType => AccountType.MAXI_SAVINGS;

        public override decimal InterestEarned()
        {
            decimal amount = sumTransactions();

            if (amount <= 1000)
                return amount * 0.02m;
            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05m;
            return 70 + (amount - 2000) * 0.1m;
        }

    }
}
