using System;
using App.Data;
using App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace App.UnitTests
{
    [TestClass]
    public class CustomerServiceTests
    {

        [TestMethod]
        public void AddCustomer_HasCreditLimitAndCreditLimitZero_ReturnsFalse()
        {
            // Arrange
            string firstName = "Norbert";
            string surname = "Csibi";
            string emailAddress = "norbertcsibitech@gmail.com";
            DateTime dateOfBirth = new DateTime(1992, 11, 21);
            int companyId = 1;

            var company = new Company { Id = 1, Name = "Norbert Ltd", Classification = Classification.Gold };

            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var mockCustomerCreditService = new Mock<ICustomerCreditService>();

            var mockCustomerService = new Mock<ICustomerService>();

            mockCompanyRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(company);

           
            
            var customerService = new CustomerService(mockCompanyRepository.Object, mockCustomerCreditService.Object);

            // Act
            var result = customerService.AddCustomer(firstName, surname, emailAddress, dateOfBirth, companyId);

            // Assert

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddCustomer_HasNoNameAndCreditLimitZero_ReturnsFalse()
        {
            // Arrange
            string firstName = String.Empty;
            string surname = "Csibi";
            string emailAddress = "norbertcsibitech@gmail.com";
            DateTime dateOfBirth = new DateTime(1992, 11, 21);
            int companyId = 1;

            var company = new Company { Id = 1, Name = "Norbert Ltd", Classification = Classification.Gold };

            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var mockCustomerCreditService = new Mock<ICustomerCreditService>();

            var mockCustomerService = new Mock<ICustomerService>();

            mockCompanyRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(company);



            var customerService = new CustomerService(mockCompanyRepository.Object, mockCustomerCreditService.Object);

            // Act
            var result = customerService.AddCustomer(firstName, surname, emailAddress, dateOfBirth, companyId);

            // Assert

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddCustomer_HasNoAtSignInTheEmailAndCreditLimitZero_ReturnsFalse()
        {
            // Arrange
            string firstName = String.Empty;
            string surname = "Csibi";
            string emailAddress = "norbertcsibitechgmail.com";
            DateTime dateOfBirth = new DateTime(1992, 11, 21);
            int companyId = 1;

            var company = new Company { Id = 1, Name = "Norbert Ltd", Classification = Classification.Gold };

            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var mockCustomerCreditService = new Mock<ICustomerCreditService>();

            var mockCustomerService = new Mock<ICustomerService>();

            mockCompanyRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(company);



            var customerService = new CustomerService(mockCompanyRepository.Object, mockCustomerCreditService.Object);

            // Act
            var result = customerService.AddCustomer(firstName, surname, emailAddress, dateOfBirth, companyId);

            // Assert

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        [Ignore]
        public void AddCustomer_HasNoCreditLimitAndCreditLimitZero_ReturnsCustomer()
        {
            // Arrange
            string firstName = "Norbert";
            string surname = "Csibi";
            string emailAddress = "norbertcsibitech@gmail.com";
            DateTime dateOfBirth = new DateTime(1992, 11, 21);
            int companyId = 1;

            var company = new Company { Id = 1, Name = "VeryImportantClient", Classification = Classification.Gold };

            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var mockCustomerCreditService = new Mock<ICustomerCreditService>();
            
            mockCompanyRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(company);

            var customerService = new CustomerService(mockCompanyRepository.Object, mockCustomerCreditService.Object);

            // Act

            var result = customerService.AddCustomer(firstName, surname, emailAddress, dateOfBirth, companyId);

            // Assert

        }
    }
}
