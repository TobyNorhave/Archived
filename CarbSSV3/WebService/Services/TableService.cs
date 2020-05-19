using Controller;
using Model;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using WebService.Requests;

namespace WebService.Services
{
    public class TableService : Service
    {
        public Table Post(CreateTableRequest request)
        {
            var tableCtr = new TableCtr();
            var tableData = new Table
            {
                NoOfSeats = request.NoOfSeats,
                TableNumber = request.TableNumber
            };
            var cafeData = new Cafe
            {
                ID = request.CafeID
            };
            tableCtr.CreateTable(tableData, cafeData);
            return tableData;
        }

        public List<Table> Get(GetAllTablesInCafeRequest request)
        {
            var tableCtr = new TableCtr();
            var cafeData = new Cafe
            {
                ID = request.ID
            };
            return tableCtr.GetAllTablesInCafe(cafeData);
        }

        public Table Get(GetTableByIDRequest request)
        {
            var tableCtr = new TableCtr();
            return tableCtr.GetTableByID(request.ID);
        }

        public Table Put(UpdateTableRequest request)
        {
            var tableCtr = new TableCtr();
            var tableData = new Table
            {
                ID = request.ID,
                NoOfSeats = request.NoOfSeats,
                TableNumber = request.TableNumber
            };
            tableCtr.UpdateTable(tableData);
            return tableData;
        }

        public void Delete(DeleteTableRequest request)
        {
            var tableCtr = new TableCtr();
            tableCtr.Delete(request.ID);
        }
    }
}

