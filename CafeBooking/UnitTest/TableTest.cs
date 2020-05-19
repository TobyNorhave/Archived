using CafeBooking.Model;
using Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Table table = new Table();

            table.Available = true;
            table.NoOfSeats = 10;
            table.TableNo = 1;

            TableDb tableDb = new TableDb();

            tableDb.Create(table);

            Assert.AreEqual(table, tableDb.Get(1));

        }
    }
}
