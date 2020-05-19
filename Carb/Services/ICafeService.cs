using Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface ICafeService
    {
        [OperationContract]
        void Create(Cafe cafe);

        [OperationContract]
        Cafe GetCafeWithAdmin(int ID);

        [OperationContract]
        Cafe GetCafe(int ID);

        [OperationContract]
        List<Cafe> GetAll();

        [OperationContract]
        void Update(Cafe cafe);

        [OperationContract]
        void Delete(int ID);
    }
}
