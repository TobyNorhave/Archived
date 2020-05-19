using Database;
using Model;
using System.Collections.Generic;

namespace Controller
{
    public class TableCtr : ITable<Table, Cafe>
    {
        private TableDb _tableDb;

        public TableCtr()
        {
            _tableDb = new TableDb();
        }

        public void Create(Table table)
        {
            _tableDb.Create(table);
        }

        public Table Get(int ID)
        {
            return _tableDb.Get(ID);
        }

        public void Update(Table table)
        {
            _tableDb.Update(table);
        }

        public void Delete(int ID)
        {
            _tableDb.Delete(ID);
        }

        public List<Table> GetAll(Cafe cafe)
        {
            return _tableDb.GetAll(cafe);
        }


    }
}
