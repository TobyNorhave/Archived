using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.ServiceClient.Web;
using Web.Requests;
using Model;

namespace UnitTest
{
    [TestClass]
    public class CafeTableTest
    {
        [TestMethod]
        public void CafeTableIntergrationTest()
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Host"]);
            var CafeTable = new CafeTable();
            var CafeTableObj = new CafeTable();

            //Creates test CafeTable with CafeID(4)
            var ctRes = client.Post(new CafeTableRequest
            {
                TableNo = 10,
                NoOfSeats = 4,
                CafeId = 4
            });

            foreach (var item in ctRes.Result)
            {
                CafeTable = item;
            }

            Assert.IsNotNull(CafeTable.Id);

            //Gets the CafeTable by Id
            var ctResId = client.Get(new CafeTableRequest
            {
                Id = CafeTable.Id
            });

            foreach (var item in ctResId.Result)
            {
                CafeTableObj = item;
            }

            Assert.AreEqual(CafeTableObj.Id, CafeTable.Id);

            //Gets all CafeTable in the Cafe(4)
            var ctResByCId = client.Get(new GetAllTablesInCafeRequest
            {
                CafeId = 4
            });

            Assert.IsNotNull(ctResByCId.Result);

            //Updates CafeTable by Id with different TableNo
            var ctResUp = client.Put(new CafeTableRequest
            {
                Id = CafeTable.Id,
                TableNo = 11,
                NoOfSeats = 10
            });

            foreach (var item in ctResUp.Result)
            {
                CafeTableObj = item;
            }

            Assert.AreNotSame(CafeTableObj, CafeTable);

            //Deletes the test CafeTable after the test
            client.Delete(new DeleteTableRequest
            {
                Id = CafeTable.Id
            });
        }
    }
}
