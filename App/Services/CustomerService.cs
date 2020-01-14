using App.Data;
using App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class CustomerService : ICustomerService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICustomerCreditService _customerCreditService;

        // Injecting the ICompanyRepository to the class.
        public CustomerService(ICompanyRepository companyRepository, ICustomerCreditService customerCreditService)
        {
            _companyRepository = companyRepository;
            _customerCreditService = customerCreditService;
        }

        public bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            var isParametersValid = checkCustomerParameters(firname, surname, email);

            if (!isParametersValid)
            {
                return isParametersValid;
            }

            var customer = enrichCustomerDetail(firname, surname, email, dateOfBirth, companyId);

            if (customer == null)
            {
                return false;
            }

            // It says in the PDF that this static class/method shouldn`t be changed, so i didn`t know exactly how to mock this
            CustomerDataAccess.AddCustomer(customer);

            return true;
        }

        public bool checkCustomerParameters(string firname, string surname, string email)
        {
            if (string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        public Customer enrichCustomerDetail(string firname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return null;
            }

            // var companyRepository = new CompanyRepository();
            var company = _companyRepository.GetById(companyId);

            var customer = new Customer
            {
                Company = company,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname
            };

            // I did this because maybe we have a company that is not stored in our DB perhaps and it will throw a null exception
            if (!(company == null))
            {
                if (company.Name == "VeryImportantClient")
                {
                    // Skip credit check
                    customer.HasCreditLimit = false;
                }
                else if (company.Name == "ImportantClient")
                {
                    // Do credit check and double credit limit
                    customer.HasCreditLimit = true;
                    using (_customerCreditService)
                    {
                        var creditLimit = _customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        creditLimit = creditLimit * 2;
                        customer.CreditLimit = creditLimit;
                    }
                }
                else
                {
                    // Do credit check
                    customer.HasCreditLimit = true;

                    // ICustomerCreditService is being disposed after using, so everything which is unmanaged will be out of scope.
                    using (_customerCreditService)
                    {
                        var creditLimit = _customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        customer.CreditLimit = creditLimit;
                    }
                }
            }
           

            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return null;
            }

            return customer;
        }
    }
}
