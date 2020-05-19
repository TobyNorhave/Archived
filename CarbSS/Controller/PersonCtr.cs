using Database;
using Model;
using System.Collections.Generic;

namespace Controller
{
    public class PersonCtr : IPerson<Customer, Administrator>
    {
        private PersonDb _personDb = new PersonDb();

        public void CreateCustomer(Customer customer) => _personDb.CreateCustomer(customer);

        public Customer GetCustomerByID(int ID) => _personDb.GetCustomerByID(ID);

        public List<Customer> GetAllCustomers() => _personDb.GetAllCustomers();

        public void UpdateCustomer(Customer customer) => _personDb.UpdateCustomer(customer);

        public void CreateAdmin(Administrator admin) => _personDb.CreateAdmin(admin);

        public Administrator GetAdminByID(int ID) => _personDb.GetAdminByID(ID);

        public List<Administrator> GetAllAdmins() => _personDb.GetAllAdmins();

        public void UpdateAdmin(Administrator admin) => _personDb.UpdateAdmin(admin);

        public void Delete(int ID) => _personDb.Delete(ID);
    }
}
