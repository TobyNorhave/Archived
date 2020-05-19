using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using WebService.Requests;

namespace UnitTests
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void TestAllCustomerMethods()
        {
            IRestClient client = new JsonServiceClient(ConfigurationManager.AppSettings["host"]);

            List<Customer> allCustomers = client.Get(new GetAllCustomersRequest());
            Assert.IsNotNull(allCustomers.Count);

            var newCustomer = client.Post(new CreateCustomerRequest
            {
                FName = "CarbTest",
                LName = "CustCarb",
                PhoneNo = "99887766",
                Email = "CarbCust@gmail.com",
                VIP = false
            });

            Assert.AreEqual(newCustomer.FName, "CarbTest");

            var oldAllCustomers = allCustomers;
            allCustomers = client.Get(new GetAllCustomersRequest());
            Assert.AreNotEqual(oldAllCustomers.Count, allCustomers.Count);

            var customer = client.Get(new GetCustomerByPhoneNoRequest { PhoneNo = newCustomer.PhoneNo });
            Assert.AreEqual(customer.FName, newCustomer.FName);

            customer.FName = "TestCarb";
            client.Put(new UpdateCustomerRequest { ID = customer.ID, FName = customer.FName, LName = customer.LName, PhoneNo = customer.PhoneNo, Email = customer.Email, VIP = true });
            customer = client.Get(new GetCustomerByPhoneNoRequest { PhoneNo = customer.PhoneNo });
            Assert.AreNotSame(customer.FName, newCustomer.FName);


            client.Delete(new DeleteCustomerRequest { ID = customer.ID });
        }

        [TestMethod]
        public void TestAllAdminMethods()
        {
            IRestClient client = new JsonServiceClient(ConfigurationManager.AppSettings["host"]);

            List<Administrator> allAdmins = client.Get(new GetAllAdminsRequest());
            Assert.IsNotNull(allAdmins.Count);

            var newAdmin = client.Post(new CreateAdminRequest
            {
                FName = "CarbTestAdmin",
                LName = "AdminCarb",
                PhoneNo = "99887777",
                Email = "CarbCust@gmail.com",

            });

            Assert.AreEqual(newAdmin.FName, "CarbTestAdmin");

            var AllOldAdmins = allAdmins;
            allAdmins = client.Get(new GetAllAdminsRequest());
            Assert.AreNotEqual(AllOldAdmins.Count, allAdmins.Count);

            var admin = client.Get(new GetAdminByPhoneNoRequest { PhoneNo = newAdmin.PhoneNo });
            Assert.AreEqual(admin.FName, newAdmin.FName);

            admin.FName = "TestCarbAdmin";
            client.Put(new UpdateAdminRequest
            {
                ID = admin.ID,
                FName = admin.FName,
                LName = admin.LName,
                PhoneNo = admin.PhoneNo,
                Email = admin.Email
            });
            admin = client.Get(new GetAdminByPhoneNoRequest { PhoneNo = admin.PhoneNo });
            Assert.AreNotSame(admin.FName, newAdmin.FName);


            client.Delete(new DeleteAdminRequest { ID = admin.ID });
        }
    }
}
