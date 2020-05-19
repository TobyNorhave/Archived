using System.Collections.Generic;

namespace Database
{
    public interface ITable<Table, Cafe>
    {
        void Create(Table table);
        Table Get(int ID);
        void Update(Table table);
        void Delete(int ID);
        List<Table> GetAll(Cafe cafe);

    }
}
