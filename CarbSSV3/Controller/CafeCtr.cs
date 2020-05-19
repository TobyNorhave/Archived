using Database;
using Model;
using System.Collections.Generic;

namespace Controller
{
    public class CafeCtr : ICafe<Cafe>
    {
        private CafeDb _cafeDb = new CafeDb();

        public void CreateCafe(Cafe cafe) => _cafeDb.CreateCafe(cafe);

        public List<Cafe> GetCafesByAdminID(int ID) => _cafeDb.GetCafesByAdminID(ID);

        public Cafe GetCafeByID(int ID) => _cafeDb.GetCafeByID(ID);

        public List<Cafe> GetAllCafes() => _cafeDb.GetAllCafes();

        public List<Cafe> GetAllCafesByZip(int zipID) => _cafeDb.GetAllCafesByZip(zipID);

        public void UpdateCafe(Cafe cafe) => _cafeDb.UpdateCafe(cafe);

        public void Delete(int ID) => _cafeDb.Delete(ID);
    }
}
