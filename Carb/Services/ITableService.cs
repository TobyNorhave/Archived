using Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    interface ITableService
    {
        [OperationContract]
        void Create(Table table);

        [OperationContract]
        Table Get(int ID);

        [OperationContract]
        void Update(Table table);

        [OperationContract]
        void Delete(int ID);

        [OperationContract]
        List<Table> GetAll(Cafe cafe);
    }
}
