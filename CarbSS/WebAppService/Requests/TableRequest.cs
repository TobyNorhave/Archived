using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppService.Requests
{
    [Route("/carb/table/update/{ID}", Verbs = "PUT")]
    [Route("/carb/table/create", Verbs = "POST")]
    public class TableRequestPP
    {
        public int ID { get; set; }
        public int NoOfSeats { get; set; }
        public int TableNumber { get; set; }
    }

    [Route("/carb/table/cafe/{ID}", Verbs = "GET")]
    public class GetAllTablesInCafeRequest
    {
        public int ID { get; set; }
    }

    [Route("/carb/table/{ID}", Verbs = "GET")]
    public class GetTableByIDRequest
    {
        public int ID { get; set; }
    }

    [Route("/carb/table/{ID}", Verbs = "DELETE")]
    public class DeleteTableRequest
    {
        public int ID { get; set; }
    }

}