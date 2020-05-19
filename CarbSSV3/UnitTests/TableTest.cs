using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using WebService.Requests;

namespace UnitTests
{
    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void TestAllTableMethods()
        {
            IRestClient client = new JsonServiceClient(ConfigurationManager.AppSettings["host"]);

            List<Table> allTables = client.Get(new GetAllTablesInCafeRequest { ID = 1 });
            Assert.IsNotNull(allTables.Count);

            var newTable = client.Post(new CreateTableRequest
            {
                NoOfSeats = 2,
                TableNumber = 666,
                CafeID = 1
            });

            Assert.AreEqual(newTable.TableNumber, 666);

            allTables = client.Get(new GetAllTablesInCafeRequest { ID = 1 });
            var matchedTable = new Table();
            foreach (var item in allTables)
            {
                if (item.TableNumber == newTable.TableNumber)
                {
                    matchedTable = item;
                }
            }

            Assert.IsNotNull(matchedTable.ID);

            matchedTable.NoOfSeats = 22;
            client.Put(new UpdateTableRequest
            {
                ID = matchedTable.ID,
                NoOfSeats = matchedTable.NoOfSeats,
                TableNumber = matchedTable.TableNumber
            });

            var tableByID = client.Get(new GetTableByIDRequest { ID = matchedTable.ID });
            Assert.AreEqual(tableByID.NoOfSeats, matchedTable.NoOfSeats);

            client.Delete(new DeleteTableRequest { ID = tableByID.ID});
        }
    }
}
