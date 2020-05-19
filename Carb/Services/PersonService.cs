using System.Collections.Generic;
using System.ServiceModel;
using Controller;
using Model;

namespace Services
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    class PersonService : IPersonService
    {
        private PersonCtr _personCtr;

        public PersonService()
        {
            _personCtr = new PersonCtr();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void CreateAdmin(Administrator admin)
        {
            _personCtr.CreateAdmin(admin);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void CreateCustomer(Customer customer)
        {
            _personCtr.CreateCustomer(customer);
        }

        public void Delete(int ID)
        {
            _personCtr.Delete(ID);
        }

        public Administrator GetAdminByID(int ID)
        {
            return _personCtr.GetAdminByID(ID);
        }

        public List<Administrator> GetAllAdmins()
        {
            return _personCtr.GetAllAdmins();
        }

        public List<Customer> GetAllCustomers()
        {
            return _personCtr.GetAllCustomers();
        }

        public Customer GetCustomerByID(int ID)
        {
            return _personCtr.GetCustomerByID(ID);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateAdmin(Administrator admin)
        {
            _personCtr.UpdateAdmin(admin);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateCustomer(Customer customer)
        {
            _personCtr.UpdateCustomer(customer);
        }
    }
}
