using Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    interface IPersonService
    {
        [OperationContract]
        void CreateCustomer(Customer customer);

        [OperationContract]
        Customer GetCustomerByID(int ID);

        [OperationContract]
        List<Customer> GetAllCustomers();

        [OperationContract]
        void UpdateCustomer(Customer customer);

        [OperationContract]
        void CreateAdmin(Administrator admin);

        [OperationContract]
        Administrator GetAdminByID(int ID);

        [OperationContract]
        List<Administrator> GetAllAdmins();

        [OperationContract]
        void UpdateAdmin(Administrator admin);

        [OperationContract]
        void Delete(int ID);

    }
}
