using Model;
using ServiceStack.ServiceHost;
using System.Collections.Generic;

namespace WebService.Requests
{
    [Route("/admin/create", Verbs = "POST")]
    public class CreateAdminRequest : IReturn<Administrator>
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }

    [Route("/admin/{PhoneNo}", Verbs = "GET")]
    public class GetAdminByPhoneNoRequest : IReturn<Administrator>
    {
        public string PhoneNo { get; set; }
    }

    [Route("/admin", Verbs = "GET")]
    public class GetAllAdminsRequest : IReturn<List<Administrator>>
    {

    }

    [Route("/admin/update/{ID}", Verbs = "PUT")]
    public class UpdateAdminRequest : IReturn<Administrator>
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }

    [Route("/admin/{ID}", Verbs = "DELETE")]
    public class DeleteAdminRequest : IReturnVoid
    {
        public int ID { get; set; }
    }
}