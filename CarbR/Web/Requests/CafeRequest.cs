using Model;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;

namespace Web.Requests
{
    [Route("/cafe/{Id}","GET")]
    [Route("/cafe", "POST, PUT")]
    public class CafeRequest : IReturn<CafeResponse>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Address { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string Type { get; set; }
        public string PhoneNo { get; set; }
        public int? UserId { get; set; }
        public string Description { get; set; }
    }

    [Route("/cafe/user/{UserId}", "GET")]
    public class GetCafesByUserId : IReturn<CafeResponse>
    {
        public int? UserId { get; set; }
    }

    [Route("/cafe/zip/{Zip}", "GET")]
    public class GetCafeByZipRequest : IReturn<CafeResponse>
    {
        public string Zip { get; set; }
    }

    [Route("/cafe/delete/{Id}", "DELETE")]
    public class DeleteCafeRequest : IReturnVoid
    {
        public int? Id { get; set; }
    }

    public class CafeResponse
    {
        public IEnumerable<Cafe> Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Automatic exception handling
    }
}