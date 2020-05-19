using CafeBooking.Model;
using Database;
using Database.Interface;
using System.Collections.Generic;

namespace Controller
{
    public class CafeCtr : ICRUD<Cafe>
    {
        private CafeDb _cafeDb = new CafeDb();
        public void Create(Cafe entity)
        {
            _cafeDb.Create(entity);
        }

        public void Delete(int ID)
        {
            _cafeDb.Delete(ID);
        }

        public Cafe Get(int ID)
        {
            return _cafeDb.Get(ID);
        }

        public IEnumerable<Cafe> GetAll()
        {
            return _cafeDb.GetAll();
        }

        public void Update(int ID)
        {
            _cafeDb.Update(ID);
        }
    }
}
