using System.Collections.Generic;

namespace Database
{
    public interface IPerson<Customer, Administrator>
    {
        void CreateCustomer(Customer customer);
        Customer GetCustomerByPhoneNo(string PhoneNo);
        List<Customer> GetAllCustomers();
        void UpdateCustomer(Customer customer);
        void CreateAdmin(Administrator admin);
        Administrator GetAdminByPhoneNo(string PhoneNo);
        List<Administrator> GetAllAdmins();
        void UpdateAdmin(Administrator admin);
        void Delete(int ID);

    }
}
