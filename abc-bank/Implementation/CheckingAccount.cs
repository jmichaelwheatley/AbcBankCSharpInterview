using System;
using System.Linq;
using System.Collections.Generic;
using abc_bank.TypeDefinitions;
using abc_bank.Abstractions.Classes;

namespace abc_bank.Implementation
{
    public class CheckingAccount : AccountBase
    {
        public override AccountType GetAccountType => AccountType.CHECKING;
    }
}
