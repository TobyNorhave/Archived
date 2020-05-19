using Controller;
using Model;
using ServiceStack;
using System.Collections.Generic;
using WebAppService.Requests;

namespace WebAppService.Services
{
    public class CustomerService : Service
    {
        public Customer Post(CustomerRequestPP request)
        {
            var personCtr = new PersonCtr();
            var customerData = new Customer()
            {
                ID = request.ID,
                FName = request.FName,
                LName = request.LName,
                PhoneNo = request.PhoneNo,
                Email = request.Email,
                VIP = request.VIP
            };
            personCtr.CreateCustomer(customerData);
            return customerData;
        }

        public List<Customer> Get(GetAllCustomersRequest request)
        {
            var personCtr = new PersonCtr();
            return personCtr.GetAllCustomers();
        }

        public Customer Get(GetCustomerByIDRequest request)
        {
            var personCtr = new PersonCtr();
            return personCtr.GetCustomerByID(request.ID);
        }

        public Customer Put(CustomerRequestPP request)
        {
            var personCtr = new PersonCtr();
            var customerData = new Customer()
            {
                FName = request.FName,
                LName = request.LName,
                PhoneNo = request.PhoneNo,
                Email = request.Email,
                VIP = request.VIP
            };
            personCtr.UpdateCustomer(customerData);
            return customerData;
        }

        public void Delete(DeleteCustomerRequest request)
        {
            var personCtr = new PersonCtr();
            personCtr.Delete(request.ID);
        }


    }
}