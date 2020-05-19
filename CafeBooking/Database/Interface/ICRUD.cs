using System.Collections.Generic;

namespace Database.Interface
{
    public interface ICRUD <T>
    {
        void Create(T entity);
        T Get(int ID);
        IEnumerable<T> GetAll();
        void Update(int ID);
        void Delete(int ID);
    }
}
