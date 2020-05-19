using Database;
using Model;
using System.Collections.Generic;

namespace Controller
{
    public class CafeCtr : ICafe<Cafe>
    {
        private CafeDb _cafeDb = new CafeDb();

        public void Create(Cafe cafe)
        {
            _cafeDb.Create(cafe);
        }

        public Cafe GetCafeWithAdmin(int ID)
        {
            return _cafeDb.GetCafeWithAdmin(ID);
        }

        public Cafe GetCafe(int ID)
        {
            return _cafeDb.GetCafe(ID);
        }

        public List<Cafe> GetAll()
        {
            return _cafeDb.GetAll();
        }

        public void Update(Cafe cafe)
        {
            _cafeDb.Update(cafe);
        }

        public void Delete(int ID)
        {
            _cafeDb.Delete(ID);
        }
    }
}
