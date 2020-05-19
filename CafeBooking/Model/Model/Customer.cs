namespace CafeBooking.Model
{
    public class Customer : Person
    {
        public bool VIP { get; set; }

        public Customer(int ID, string Name, string PhoneNo, string Email, bool VIP) : base(ID, Name, PhoneNo, Email)
        {
            base.ID = ID;
            base.Name = Name;
            base.PhoneNo = PhoneNo;
            base.Email = Email;
            this.VIP = VIP;
        }
    }
}