using Database;
using Model;
using ServiceStack.ServiceInterface;
using System;
using Web.Requests;

namespace Web.Services
{
    public class CafeTableService : Service
    {

        public object Post(CafeTableRequest request)
        {
            DbCafeTable dbCafeTable = new DbCafeTable();

            var cafeTable = new CafeTable
            {
                TableNo = request.TableNo.Value,
                NoOfSeats = request.NoOfSeats.Value
            };
            var cafeTableId = dbCafeTable.CreateTable(cafeTable, request.CafeId.Value);
            cafeTable.Id = cafeTableId;
            return new CafeTableResponse { Result = new CafeTable[] { cafeTable } };
        }

        public object Get(CafeTableRequest request)
        {
            DbCafeTable dbCafeTable = new DbCafeTable();
            if (request.Id.HasValue)
            {
                var cafeTable = dbCafeTable.GetTableByID(request.Id.Value);
                return new CafeTableResponse { Result = new CafeTable[] { cafeTable } };
            }
            else
            {
                throw new Exception("Please enter a valid ID");
            }
        }

        public object Get(GetAllTablesInCafeRequest request)
        {
            DbCafeTable dbCafeTable = new DbCafeTable();
            if (request.CafeId.HasValue)
            {
                return new CafeTableResponse { Result = dbCafeTable.GetAllTablesInCafe(request.CafeId.Value) };
            }
            else
            {
                throw new Exception("Please enter a valid CafeID");
            }
        }


        public object Put(CafeTableRequest request)
        {
            DbCafeTable dbCafeTable = new DbCafeTable();
            if (request.Id.HasValue)
            {
                var cafeTable = new CafeTable
                {
                    Id = request.Id.Value,
                    TableNo = request.TableNo.Value,
                    NoOfSeats = request.NoOfSeats.Value
                };
                dbCafeTable.UpdateTable(cafeTable);
                var updatedCafeTable = dbCafeTable.GetTableByID(cafeTable.Id);
                return new CafeTableResponse { Result = new CafeTable[] { updatedCafeTable } };
            }
            else
            {
                throw new Exception("Please input a vaild ID for the Table!");
            }

        }

        public void Delete(DeleteTableRequest request)
        {
            DbCafeTable dbCafeTable = new DbCafeTable();
            dbCafeTable.Delete(request.Id.Value);
        }
    }
}