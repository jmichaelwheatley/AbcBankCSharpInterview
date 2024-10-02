using abc_bank.Models;
using abc_bank.Models.Results;
using System;

namespace abc_bank.Abstractions.Interfaces
{
    public interface ICustomerCreationService
    {
        CreateCustomerResult CreateCustomer(CreateCustomerData customerData);
    }
}
