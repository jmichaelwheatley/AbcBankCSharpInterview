using System;
using System.Collections.Generic;
using System.Linq;
using abc_bank.Abstractions.Interfaces;
using abc_bank.Implementation.Services;

namespace abc_bank.Other
{
    public class MyFakeResolver
    {
        public T ResolveFor<T>() where T : class
        {
            object resolvedData = null;
            switch (typeof(T))
            {
                case Type testDependency when testDependency == typeof(ICustomerCreationService):
                    resolvedData = new CustomerCreationService();
                    break;              
            }

            return resolvedData == null ? default(T) : resolvedData as T;
        }
    }
}
