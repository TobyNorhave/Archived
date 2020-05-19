using Database;
using Model;
using System.Collections.Generic;

namespace Controller
{
    public class TableCtr : ITable<Table, Cafe>
    {
        private TableDb _tableDb = new TableDb();

        public void CreateTable(Table table, Cafe cafe) => _tableDb.CreateTable(table, cafe);

        public Table GetTableByID(int ID) => _tableDb.GetTableByID(ID);

        public List<Table> GetAllTablesInCafe(Cafe cafe) => _tableDb.GetAllTablesInCafe(cafe);

        public void UpdateTable(Table table) => _tableDb.UpdateTable(table);

        public void Delete(int ID) => _tableDb.Delete(ID);
    }
}
