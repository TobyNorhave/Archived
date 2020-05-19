using System.Collections.Generic;

namespace Database
{
    public interface ICafe<Cafe>
    {
        void Create(Cafe cafe);
        Cafe GetCafeWithAdmin(int ID);
        Cafe GetCafe(int ID);
        List<Cafe> GetAll();
        void Update(Cafe cafe);
        void Delete(int ID);

    }
}
