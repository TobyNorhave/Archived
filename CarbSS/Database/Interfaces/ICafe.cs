using System.Collections.Generic;

namespace Database
{
    public interface ICafe<Cafe>
    {
        void CreateCafe(Cafe cafe);
        List<Cafe> GetCafesByAdminID(int ID);
        Cafe GetCafeByID(int ID);
        List<Cafe> GetAllCafes();
        List<Cafe> GetAllCafesByZip(int zipID);
        void UpdateCafe(Cafe cafe);
        void Delete(int ID);
    }
}
