using Model;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;

namespace WebService.Requests
{
    [Route("/cafe/create", Verbs = "POST")]
    public class CreateCafeRequest : IReturn<Cafe>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal PriceRange { get; set; }
        public decimal Rating { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public int ZipID { get; set; }
        public int TypeID { get; set; }
        public int AdminID { get; set; }
    }

    [Route("/cafe/admin/{ID}", Verbs = "GET")]
    public class GetCafesByAdminIDRequest : IReturn<Cafe>
    {
        public int ID { get; set; }
    }

    [Route("/cafe/{ID}", Verbs = "GET")]
    public class GetCafeByIDRequest : IReturn<Cafe>
    {
        public int ID { get; set; }
    }

    [Route("/cafe", Verbs = "GET")]
    public class GetAllCafesRequest : IReturn<List<Cafe>>
    {

    }

    [Route("/cafe/zip/{ZipID}", Verbs = "GET")]
    public class GetAllCafesByZipRequest : IReturn<List<Cafe>>
    {
        public int ZipID { get; set; }
    }

    [Route("/cafe/update/{ID}", Verbs = "PUT")]
    public class UpdateCafeRequest : IReturn<Cafe>
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
        public int AdminID { get; set; }
    }

    public class DeleteCafeRequest : IReturnVoid
    {
        public int ID { get; set; }
    }
}