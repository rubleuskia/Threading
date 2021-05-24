using System;
using System.Collections.Generic;

namespace Xml
{
    class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User { Name = "Bill Gates", Age = 48, Company = "Microsoft" };
            User user2 = new User { Name = "Larry Page", Age = 42, Company = "Google" };
            List<User> users = new List<User> { user1, user2 };

            // XmlService.PrintXml();
            // XmlService.ParseUsers();
            // XmlService.UpdateXml();
            // XmlService.Xpath();
            // XmlService.LinqToXml1();
            // XmlService.LinqToXml2();
            // XmlService.LinqToXml3();
            // XmlService.LinqToXml4();
            // XmlService.LinqToXml5();

            // XmlSerialization.Run1();
            // XmlSerialization.Run2();
            Console.ReadKey();
        }
    }
}
