using System;
using System.Collections.Generic;

namespace abc_bank.Models
{
    public class CreateCustomerData
    {
        public readonly string FirstName;
        public readonly string LastName;
        public IList<CreateCustomerInitialAccountData> Accounts => _accounts.AsReadOnly();
        private List<CreateCustomerInitialAccountData> _accounts;

        public CreateCustomerData(string firstName, string lastName, List<CreateCustomerInitialAccountData> accounts)
        {
            FirstName = firstName;
            LastName = lastName;
            _accounts = accounts ?? new List<CreateCustomerInitialAccountData>(); 
        }
    }
}
