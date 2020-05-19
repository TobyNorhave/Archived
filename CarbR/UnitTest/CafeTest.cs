using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using ServiceStack.ServiceClient.Web;
using Web.Requests;

namespace UnitTest
{
    [TestClass]
    public class CafeTest
    {

        [TestMethod]
        public void CafeIntergrationTest()
        {
            JsonServiceClient Client = new JsonServiceClient(ConfigurationManager.AppSettings["Host"]);
            var Cafe = new Cafe();
            var CafeObj = new Cafe();


            //Creates test Cafe with UserId(3)
            var cRes = Client.Post(new CafeRequest
            {
                Name = "Carb Test Cafe",
                Address = "Testvej 999",
                Zip = "9000",
                OpenTime = DateTime.Now,
                CloseTime = DateTime.Now.AddHours(6),
                Description = "This is a test Cafe created by CafeTest!",
                PhoneNo = "22446688",
                Type = "Test Mexikansk",
                UserId = 3
            });

            foreach (var item in cRes.Result)
            {
                Cafe = item;
            }

            Assert.IsNotNull(Cafe.Id);

            //Gets the test cafe by UserId.
            var cResUserId = Client.Get(new GetCafesByUserId
            {
                UserId = 3
            });

            foreach (var item in cResUserId.Result)
            {
                CafeObj = item;
            }

            Assert.AreEqual(CafeObj.Name, Cafe.Name);

            //Gets test Cafe based on Id
            var cResId = Client.Get(new CafeRequest
            {
                Id = Cafe.Id
            });

            foreach (var item in cResId.Result)
            {
                CafeObj = item;
            }

            Assert.AreEqual(CafeObj.Id, Cafe.Id);

            //Get all cafes
            var cResAll = Client.Get(new CafeRequest
            {
                Id = -1
            });

            Assert.IsNotNull(cResAll.Result);

            //Gets all cafes by Zip(9000)
            var cResByZip = Client.Get(new GetCafeByZipRequest
            {
                Zip = "9000"
            });

            Assert.IsNotNull(cResByZip.Result);

            //Updates the test Cafe with a different name
            var cResUp = Client.Put(new CafeRequest
            {
                Id = Cafe.Id,
                Name = "Carb Updatet TestCafe",
                Address = Cafe.Address,
                Zip = Cafe.Zip,
                OpenTime = Cafe.OpenTime,
                CloseTime = Cafe.CloseTime,
                Description = Cafe.Description,
                PhoneNo = Cafe.PhoneNo,
                Type = Cafe.Type,
                UserId = 3
            });

            foreach (var item in cResUp.Result)
            {
                CafeObj = item;
            }

            Assert.AreNotSame(CafeObj, Cafe);

            //Deletes the test Cafe after the tests.
            Client.Delete(new DeleteCafeRequest
            {
                Id = Cafe.Id
            });
        }
    }
}
