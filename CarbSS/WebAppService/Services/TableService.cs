using Controller;
using Model;
using ServiceStack;
using System.Collections.Generic;
using WebAppService.Requests;

namespace WebAppService.Services
{
    public class TableService : Service
    {
        public Table Post(TableRequestPP request)
        {
            var tableCtr = new TableCtr();
            var tableData = new Table
            {
                ID = request.ID,
                NoOfSeats = request.NoOfSeats,
                TableNumber = request.TableNumber
            };
            var requestPP = new CafeRequestPP();
            var cafeData = new Cafe
            {
                ID = requestPP.ID,
                Name = requestPP.Name,
                Address = requestPP.Address,
                PriceRange = requestPP.PriceRange,
                Rating = requestPP.Rating,
                OpenTime = requestPP.OpenTime,
                CloseTime = requestPP.CloseTime,
                ZipID = requestPP.ZipID,
                TypeID = requestPP.TypeID
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

        public Table Put(TableRequestPP request)
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

