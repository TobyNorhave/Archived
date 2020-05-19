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
    public class CafeTest
    {
        [TestMethod]
        public void TestAllCafeMethods()
        {
            IRestClient client = new JsonServiceClient(ConfigurationManager.AppSettings["host"]);

            List<Cafe> allCafes = client.Get(new GetAllCafesRequest());
            Assert.IsNotNull(allCafes.Count);

            var newCafe = client.Post(new CreateCafeRequest
            {
                Name = "CafeCarbTest",
                Address = "Testgade V. Carb",
                PriceRange = 150,
                Rating = 5,
                OpenTime = DateTime.Now,
                CloseTime = DateTime.Now,
                ZipID = 9000,
                TypeID = 1,
                AdminID = 1
            });

            Assert.AreEqual(newCafe.Name, "CafeCarbTest");

            var allOldCafes = allCafes;
            allCafes = client.Get(new GetAllCafesRequest());
            Assert.AreNotEqual(allOldCafes.Count, allCafes.Count);

            List<Cafe> cafesByZip = client.Get(new GetAllCafesByZipRequest { ZipID = 9000});
            var matchedCafe = new Cafe();
            foreach (var item in cafesByZip)
            {
                if (item.Name == newCafe.Name)
                {
                    matchedCafe = item;
                }
            }
            Assert.AreEqual(matchedCafe.Name, newCafe.Name);

            matchedCafe.Name = "CafeTestCarb";
            client.Put(new UpdateCafeRequest
            {
                ID = matchedCafe.ID,
                Name = matchedCafe.Name,
                Address = matchedCafe.Address,
                PriceRange = matchedCafe.PriceRange,
                Rating = matchedCafe.Rating,
                OpenTime = matchedCafe.OpenTime,
                CloseTime = matchedCafe.CloseTime,
                ZipID = matchedCafe.ZipID,
                TypeID = matchedCafe.TypeID,
                AdminID = 1,
            });

            var cafe = client.Get(new GetCafeByIDRequest { ID = matchedCafe.ID });
            Assert.AreNotEqual(cafe.Name, newCafe.Name);

            client.Delete(new DeleteCafeRequest { ID = cafe.ID});
        }
    }
}
