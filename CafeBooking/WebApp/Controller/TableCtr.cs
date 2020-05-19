using CafeBooking.Model;
using Database;
using Database.Interface;
using System.Collections.Generic;

namespace Controller
{
    public class TableCtr : ICRUD<Table>
    {
        private TableDb _tableDb = new TableDb();
        public void Create(Table entity)
        {
            _tableDb.Create(entity);
        }

        public void Delete(int ID)
        {
            _tableDb.Delete(ID);
        }

        public Table Get(int ID)
        {
            return _tableDb.Get(ID);
        }

        public IEnumerable<Table> GetAll()
        {
            return _tableDb.GetAll();
        }

        public void Update(int ID)
        {
            _tableDb.Update(ID);
        }
    }
}
