using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Xml
{
    public class XmlService
    {
        public static void PrintXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("users1.xml");

            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;

            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                // получаем атрибут name
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("name");
                    if (attr != null)
                    {
                        Console.WriteLine(attr.Value);
                    }
                }

                // обходим все дочерние узлы элемента user
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    // если узел - company
                    if (childnode.Name == "company")
                    {
                        Console.WriteLine($"Компания: {childnode.InnerText}");
                    }

                    // если узел age
                    if (childnode.Name == "age")
                    {
                        Console.WriteLine($"Возраст: {childnode.InnerText}");
                    }
                }

                Console.WriteLine();
            }

            Console.Read();
        }

        public static void ParseUsers()
        {
            List<User> users = new List<User>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("users1.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            foreach (XmlElement xnode in xRoot)
            {
                User user = new User();
                XmlNode attr = xnode.Attributes.GetNamedItem("name");
                if (attr != null)
                {
                    user.Name = attr.Value;
                }

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "company")
                    {
                        user.Company = childnode.InnerText;
                    }

                    if (childnode.Name == "age")
                    {
                        user.Age = Int32.Parse(childnode.InnerText);
                    }
                }

                users.Add(user);
            }

            foreach (User u in users)
            {
                Console.WriteLine($"{u.Name} ({u.Company}) - {u.Age}");
            }

            Console.Read();
        }

        public static void UpdateXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("users1.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            // создаем новый элемент user
            XmlElement userElem = xDoc.CreateElement("user");

            // создаем атрибут name
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");

            // создаем элементы company и age
            XmlElement companyElem = xDoc.CreateElement("company");
            XmlElement ageElem = xDoc.CreateElement("age");

            // создаем текстовые значения для элементов и атрибута
            XmlText nameText = xDoc.CreateTextNode("Mark Zuckerberg");
            XmlText companyText = xDoc.CreateTextNode("Facebook");
            XmlText ageText = xDoc.CreateTextNode("30");

            //добавляем узлы
            nameAttr.AppendChild(nameText);
            companyElem.AppendChild(companyText);
            ageElem.AppendChild(ageText);

            userElem.Attributes.Append(nameAttr);
            userElem.AppendChild(companyElem);
            userElem.AppendChild(ageElem);

            xRoot.AppendChild(userElem);
            xDoc.Save("users1.xml");

            // deletion
            // XmlNode firstNode = xRoot.FirstChild;
            // xRoot.RemoveChild(firstNode);
            // xDoc.Save("users1.xml");
        }

        public static void Xpath()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("users1.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            // выбор всех дочерних узлов
            XmlNodeList childnodes = xRoot.SelectNodes("*");
            foreach (XmlNode n in childnodes)
            {
                Console.WriteLine(n.OuterXml);
            }

            // user nodes
            XmlNodeList userNodes = xRoot.SelectNodes("user");
            foreach (XmlNode n in userNodes)
            {
                Console.WriteLine(n.SelectSingleNode("@name").Value);
            }

            // specific user by name
            XmlNode childnode1 = xRoot.SelectSingleNode("user[@name='Bill Gates']");
            if (childnode1 != null)
            {
                Console.WriteLine(childnode1.OuterXml);
            }

            // specific user by company
            XmlNode childnode2 = xRoot.SelectSingleNode("user[company='Microsoft']");
            if (childnode2 != null)
            {
                Console.WriteLine(childnode2.OuterXml);
            }

            XmlNodeList companyNodes = xRoot.SelectNodes("//user/company");
            foreach (XmlNode n in companyNodes)
            {
                Console.WriteLine(n.InnerText);
            }
        }

        public static void LinqToXml1()
        {
            XDocument xdoc = new XDocument();

            // создаем первый элемент
            XElement iphone6 = new XElement("phone");

            // создаем атрибут
            XAttribute iphoneNameAttr = new XAttribute("name", "iPhone 6");
            XElement iphoneCompanyElem = new XElement("company", "Apple");
            XElement iphonePriceElem = new XElement("price", "40000");

            // добавляем атрибут и элементы в первый элемент
            iphone6.Add(iphoneNameAttr);
            iphone6.Add(iphoneCompanyElem);
            iphone6.Add(iphonePriceElem);

            // создаем второй элемент
            XElement galaxys5 = new XElement("phone");
            XAttribute galaxysNameAttr = new XAttribute("name", "Samsung Galaxy S5");
            XElement galaxysCompanyElem = new XElement("company", "Samsung");
            XElement galaxysPriceElem = new XElement("price", "33000");
            galaxys5.Add(galaxysNameAttr);
            galaxys5.Add(galaxysCompanyElem);
            galaxys5.Add(galaxysPriceElem);

            // создаем корневой элемент
            XElement phones = new XElement("phones");

            // добавляем в корневой элемент
            phones.Add(iphone6);
            phones.Add(galaxys5);

            // добавляем корневой элемент в документ
            xdoc.Add(phones);

            //сохраняем документ
            xdoc.Save("phones.xml");
        }

        public static void LinqToXml2()
        {
            XDocument xdoc = new XDocument(
                new XElement("phones",
                    new XElement("phone",
                        new XAttribute("name", "iPhone 6"),
                        new XElement("company", "Apple"),
                        new XElement("price", "40000")),
                    new XElement("phone",
                        new XAttribute("name", "Samsung Galaxy S5"),
                        new XElement("company", "Samsung"),
                        new XElement("price", "33000"))));

            xdoc.Save("phones2.xml");
        }

        public static void LinqToXml3()
        {
            XDocument xdoc = XDocument.Load("phones-linq.xml");
            foreach (XElement phoneElement in xdoc.Element("phones").Elements("phone"))
            {
                XAttribute nameAttribute = phoneElement.Attribute("name");
                XElement companyElement = phoneElement.Element("company");
                XElement priceElement = phoneElement.Element("price");

                if (nameAttribute != null && companyElement != null && priceElement != null)
                {
                    Console.WriteLine($"Смартфон: {nameAttribute.Value}");
                    Console.WriteLine($"Компания: {companyElement.Value}");
                    Console.WriteLine($"Цена: {priceElement.Value}");
                }

                Console.WriteLine();
            }
        }

        class Phone
        {
            public string Name { get; set; }
            public string Price { get; set; }
        }

        public static void LinqToXml4()
        {
            XDocument xdoc = XDocument.Load("phones.xml");

            var items = xdoc.Element("phones")
                .Elements("phone")
                .Where(xe => xe.Element("company").Value == "Samsung")
                .Select(xe => new Phone
                {
                    Name = xe.Attribute("name").Value,
                    Price = xe.Element("price").Value,
                });

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Name} - {item.Price}");
            }
        }

        public static void LinqToXml5()
        {
            XDocument xdoc = XDocument.Load("phones.xml");
            XElement root = xdoc.Element("phones");

            foreach (XElement xe in root.Elements("phone").ToList())
            {
                // изменяем название и цену
                if (xe.Attribute("name").Value == "Samsung Galaxy S5")
                {
                    xe.Attribute("name").Value = "Samsung Galaxy Note 4";
                    xe.Element("price").Value = "31000";
                }

                //если iphone - удаляем его
                else if (xe.Attribute("name").Value == "iPhone 6")
                {
                    xe.Remove();
                }
            }

            // добавляем новый элемент
            root.Add(new XElement("phone",
                new XAttribute("name", "Nokia Lumia 930"),
                new XElement("company", "Nokia"),
                new XElement("price", "19500")));

            xdoc.Save("pnones1.xml");
            // выводим xml-документ на консоль
            Console.WriteLine(xdoc);
            Console.Read();
        }
    }
}
