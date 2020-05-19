using System.Collections.Generic;

namespace Database
{
    public interface IPerson<Customer, Administrator>
    {
        void CreateCustomer(Customer customer);
        Customer GetCustomerByID(int ID);
        List<Customer> GetAllCustomers();
        void UpdateCustomer(Customer customer);
        void CreateAdmin(Administrator admin);
        Administrator GetAdminByID(int ID);
        List<Administrator> GetAllAdmins();
        void UpdateAdmin(Administrator admin);
        void Delete(int ID);

    }
}
