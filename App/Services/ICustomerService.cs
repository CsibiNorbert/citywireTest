using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public interface ICustomerService
    {
        bool AddCustomer(string firstName, string surname, string email, DateTime dateOfBirth, int companyId);
    }
}
