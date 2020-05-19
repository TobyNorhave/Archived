using Model;
using ServiceStack.ServiceHost;
using System.Collections.Generic;

namespace WebService.Requests
{
    [Route("/customer/create", Verbs = "POST")]
    public class CreateCustomerRequest : IReturn<Customer>
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public bool VIP { get; set; }
    }

    [Route("/customer/{PhoneNo}", Verbs = "GET")]
    public class GetCustomerByPhoneNoRequest : IReturn<Customer>
    {
        public string PhoneNo { get; set; }
    }

    [Route("/customer", Verbs = "GET")]
    public class GetAllCustomersRequest : IReturn<List<Customer>>
    {

    }

    [Route("/customer/update/{ID}", Verbs = "PUT")]
    public class UpdateCustomerRequest : IReturn<Customer>
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public bool VIP { get; set; }
    }

    [Route("/customer/{ID}", Verbs = "DELETE")]
    public class DeleteCustomerRequest : IReturnVoid
    {
        public int ID { get; set; }
    }
}