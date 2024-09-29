using System;
using System.Linq;
using System.Collections.Generic;
using abc_bank.Abstractions.Classes;
using abc_bank.TypeDefinitions;

namespace abc_bank.Implementation
{
    public class SavingsAccount : AccountBase
    {
        public override AccountType GetAccountType => AccountType.SAVINGS;

        public override decimal InterestEarned()
        {
            decimal amount = sumTransactions();

            if (amount <= 1000)
                return amount * 0.001m;
            else
                return 1 + (amount - 1000) * 0.002m;
        }
    }
}
