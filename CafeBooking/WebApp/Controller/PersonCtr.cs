using CafeBooking.Model;
using Database;
using Database.Interface;
using System.Collections.Generic;

namespace Controller
{
    public class PersonCtr : ICRUD<Person>
    {
        private PersonDb _personDb = new PersonDb();

        public void Create(Person entity)
        {
            _personDb.Create(entity);
        }

        public void Delete(int ID)
        {
            _personDb.Delete(ID);
        }

        public Person Get(int ID)
        {
            return _personDb.Get(ID);
        }

        public IEnumerable<Person> GetAll()
        {
            return _personDb.GetAll();
        }

        public void Update(int ID)
        {
            _personDb.Update(ID);
        }
    }
}
