using CarbWeb;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new JsonServiceClient("http://localhost:51283") { UserName = "Toby", Password = "pass"};
            client.Send(new AssignRoles { UserName = "Toby", Roles = new ArrayOfString("Customer"), Permissions = new ArrayOfString("GetCafe")});

            var provider = CultureInfo.InvariantCulture;
            var format = "MMM HH':'mm':'yyyy";

            int zipCode = 0;
            while (zipCode == 0)
            {
                Console.WriteLine("Enter the name of the cafe:");
                var name = Console.ReadLine();
                Console.WriteLine("Enter the address of the cafe:");
                var address = Console.ReadLine();
                Console.WriteLine("Enter the city of the cafe:");
                var city = Console.ReadLine();
                Console.WriteLine("Enter the Open Time of the cafe of the cafe | Format: MMM HH:mm:yyyy");
                var openTime = Console.ReadLine();
                var fixedOpenTime = DateTime.ParseExact(openTime, format, provider);
                Console.WriteLine("Enter the price range of the cafe:");
                var priceRange = Console.ReadLine();
                Console.WriteLine("Enter the rating of the cafe:");
                var rating = Console.ReadLine();
                Console.WriteLine("Enter the type of the cafe:");
                var type = Console.ReadLine();
                Console.WriteLine("Enter the ZipCode of the city:");
                var newZip = Console.ReadLine();
                var parsedZip = Int32.Parse(newZip);

                client.SendAsync(new Cafe { Name = name, Address = address, City = city, OpenTime = fixedOpenTime, CloseTime = fixedOpenTime.AddHours(10), PriceRange = Decimal.Parse(priceRange), Rating = Decimal.Parse(rating), Type =  type, ZipCode = parsedZip}, 
                    cafeRespone => Console.WriteLine($"Response: {cafeRespone.ToString()} | Message: {cafeRespone.Message}"),
                    (cafeResonse, exception) => Console.WriteLine(exception.Message));

                zipCode = parsedZip;
                Console.ReadKey();
            }


        }
    }
}
