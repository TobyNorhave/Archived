using Controller;
using Model;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using WebService.Requests;

namespace WebService.Services
{
    public class CustomerService : Service
    {
        public Customer Post(CreateCustomerRequest request)
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
            personCtr.CreateCustomer(customerData);
            return customerData;
        }

        public Customer Get(GetCustomerByPhoneNoRequest request)
        {
            var personCtr = new PersonCtr();
            return personCtr.GetCustomerByPhoneNo(request.PhoneNo);
        }

        public List<Customer> Get(GetAllCustomersRequest request)
        {
            var personCtr = new PersonCtr();
            return personCtr.GetAllCustomers();
        }

        public Customer Put(UpdateCustomerRequest request)
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