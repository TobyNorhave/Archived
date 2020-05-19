using Model;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System.Collections.Generic;

namespace Web.Requests
{
    [Route("/table/{Id}", "GET")]
    [Route("/table/", "POST, PUT")]
    public class CafeTableRequest : IReturn<CafeTableResponse>
    {
        public int? Id { get; set; }
        public int? TableNo { get; set; }
        public int? NoOfSeats { get; set; }
        public int? CafeId { get; set; }
    }

    [Route("/table/cafe/{CafeId}", "GET")]
    public class GetAllTablesInCafeRequest : IReturn<CafeTableResponse>
    {
        public int? CafeId { get; set; }
    }

    [Route("/table/delete", "DELETE")]
    public class DeleteTableRequest : IReturnVoid
    {
        public int? Id { get; set; }
    }

    public class CafeTableResponse
    {
        public IEnumerable<CafeTable> Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Automatic exception handling
    }
}