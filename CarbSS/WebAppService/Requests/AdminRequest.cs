using ServiceStack;

namespace WebAppService.Requests
{
    [Route("/admin/update/{ID}", Verbs = "PUT")]
    [Route("/admin/create", Verbs = "POST")]
    public class AdminRequestPP
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }

    [Route("/admin/{ID}", Verbs = "GET")]
    public class GetAdminByIDRequest
    {
        public int ID { get; set; }
    }

    [Route("/admin", Verbs = "GET")]
    public class GetAllAdminsRequest
    {

    }

    [Route("/admin/{ID}", Verbs = "DELETE")]
    public class DeleteAdminRequest
    {
        public int ID { get; set; }
    }
}
