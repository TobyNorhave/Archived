using ServiceStack;

namespace WebAppService.Requests
{
    [Route("/carb/customer/update/{ID}", Verbs = "PUT")]
    [Route("/carb/customer/create", Verbs = "POST")]
    public class CustomerRequestPP
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public bool VIP { get; set; }
    }

    [Route("/carb/customer", Verbs = "GET")]
    public class GetAllCustomersRequest
    {

    }

    [Route("/carb/customer/{ID}", Verbs = "GET")]
    public class GetCustomerByIDRequest
    {
        public int ID { get; set; }
    }



    [Route("/carb/customer/{ID}", Verbs = "DELETE")]
    public class DeleteCustomerRequest
    {
        public int ID { get; set; }
    }


}