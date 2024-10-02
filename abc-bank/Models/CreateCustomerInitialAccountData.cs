using abc_bank.TypeDefinitions;

namespace abc_bank.Models
{
    public class CreateCustomerInitialAccountData
    {
        public readonly AccountType AccountType;
        public readonly decimal DepositAmount;

        public CreateCustomerInitialAccountData(AccountType accountType, decimal depositAmount)
        {
            AccountType = accountType;
            DepositAmount = depositAmount;
        }
    }
}
