using System.Collections.Generic;
using System.ServiceModel;
using Controller;
using Model;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CafeService : ICafeService
    {
        private CafeCtr _cafeCtr;
        public CafeService()
        {
            _cafeCtr = new CafeCtr();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Create(Cafe cafe)
        {
            _cafeCtr.Create(cafe);
        }

        public void Delete(int ID)
        {
            _cafeCtr.Delete(ID);
        }

        public Cafe GetCafeWithAdmin(int ID)
        {
            return _cafeCtr.GetCafeWithAdmin(ID);
        }

        public Cafe GetCafe(int ID)
        {
            return _cafeCtr.GetCafe(ID);
        }

        public List<Cafe> GetAll()
        {
            return _cafeCtr.GetAll();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Update(Cafe cafe)
        {
            _cafeCtr.Update(cafe);
        }
    }
}

