using abc_bank.Abstractions.Classes;

namespace abc_bank.Models.Results
{
    public class CreateCustomerResult
    {
        public readonly Customer Customer;
        public readonly AccountBase[] Accounts;

        public CreateCustomerResult(Customer customer, AccountBase[] accounts)
        {
            Accounts = accounts;
            Customer = customer;
        }
    }
}
