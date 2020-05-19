using System.Collections.Generic;

namespace Database
{
    public interface ITable<Table, Cafe>
    {
        void CreateTable(Table table, Cafe cafe);
        Table GetTableByID(int ID);
        List<Table> GetAllTablesInCafe(Cafe cafe);
        void UpdateTable(Table table);
        void Delete(int ID);
    }
}
