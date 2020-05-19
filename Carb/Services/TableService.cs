using Controller;
using Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class TableService : ITableService
    {
        private TableCtr _tableCtr;

        public TableService()
        {
            _tableCtr = new TableCtr();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Create(Table table)
        {
            _tableCtr.Create(table);
        }

        public void Delete(int ID)
        {
            _tableCtr.Delete(ID);
        }

        public Table Get(int ID)
        {
            return _tableCtr.Get(ID);
        }

        public List<Table> GetAll(Cafe cafe)
        {
            return _tableCtr.GetAll(cafe);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Update(Table table)
        {
            _tableCtr.Update(table);
        }
    }
}
