using ServiceStack;
using System;

namespace WebAppService.Requests
{
    [Route("/cafe/update/{ID}", Verbs = "PUT")]
    [Route("/cafe/create", Verbs = "POST")]
    public class CafeRequestPP
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal PriceRange { get; set; }
        public decimal Rating { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public int ZipID { get; set; }
        public int TypeID { get; set; }
    }

    [Route("/cafe/admin/{ID}", Verbs = "GET")]
    public class GetCafesByAdminIDRequest
    {
        public int ID { get; set; }
    }

    [Route("/cafe/{ID}", Verbs = "GET")]
    public class GetCafeByIDRequest
    {
        public int ID { get; set; }
    }

    [Route("/cafe", Verbs = "GET")]
    public class GetAllCafesRequest
    {

    }

    [Route("/cafe/{ZipID}", Verbs = "GET")]
    public class GetAllCafesByZipeRequest
    {
        public int ZipID { get; set; }
    }

    public class DeleteCafeRequest
    {
        public int ID { get; set; }
    }
}