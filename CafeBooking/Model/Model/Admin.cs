namespace CafeBooking.Model
{
    class Admin : Person
    {
        public Admin(int ID, string Name, string PhoneNo, string Email) : base(ID, Name, PhoneNo, Email)
        {
            base.ID = ID;
            base.Name = Name;
            base.PhoneNo = PhoneNo;
            base.Email = Email;
        }
    }
}
