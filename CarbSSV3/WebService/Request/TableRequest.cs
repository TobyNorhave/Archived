using Model;
using ServiceStack.ServiceHost;
using System.Collections.Generic;

namespace WebService.Requests
{
    [Route("/table/create", Verbs = "POST")]
    public class CreateTableRequest : IReturn<Table>
    {
        public int NoOfSeats { get; set; }
        public int TableNumber { get; set; }
        public int CafeID { get; set; }
    }

    [Route("/table/{ID}", Verbs = "GET")]
    public class GetTableByIDRequest : IReturn<Table>
    {
        public int ID { get; set; }
    }

    [Route("/table/cafe/{ID}", Verbs = "GET")]
    public class GetAllTablesInCafeRequest : IReturn<List<Table>>
    {
        public int ID { get; set; }
    }

    [Route("/table/update/{ID}", Verbs = "PUT")]
    public class UpdateTableRequest : IReturn<Cafe>
    {
        public int ID { get; set; }
        public int NoOfSeats { get; set; }
        public int TableNumber { get; set; }
    }

    [Route("/table/{ID}", Verbs = "DELETE")]
    public class DeleteTableRequest : IReturnVoid
    {
        public int ID { get; set; }
    }

}