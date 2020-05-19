namespace CafeBooking.Model
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }

        public Person(string Name, string PhoneNo, string Email)
        {
            this.Name = Name;
            this.PhoneNo = PhoneNo;
            this.Email = Email;
        }

        public Person(int ID, string Name, string PhoneNo, string Email)
        {
            this.ID = ID;
            this.Name = Name;
            this.PhoneNo = PhoneNo;
            this.Email = Email;
        }
    }
}
