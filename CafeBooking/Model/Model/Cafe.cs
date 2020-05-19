namespace CafeBooking.Model
{
    public class Cafe
    {
        public int ID { get; set; }
        public int ZipID { get; set; }
        public int PriceRange { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        public Cafe(string name, int zipID, string address, int priceRange, int type)
        {
            Name = name;
            ZipID = zipID;
            Address = address;
            PriceRange = priceRange;
            Type = type;
        }
    }
}
