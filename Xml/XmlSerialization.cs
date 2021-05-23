using System;
using System.IO;
using System.Xml.Serialization;

namespace Xml
{
    public class XmlSerialization
    {
        [Serializable]
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            // стандартный конструктор без параметров
            public Person()
            { }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        public static void Run1()
        {
            // объект для сериализации
            Person person = new Person("Tom", 29);
            Console.WriteLine("Объект создан");

            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(Person));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, person);

                Console.WriteLine("Объект сериализован");
            }

            // десериализация
            using (FileStream fs = new FileStream("persons.xml", FileMode.Open))
            {
                Person newPerson = (Person)formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine($"Имя: {newPerson.Name} --- Возраст: {newPerson.Age}");
            }

            Console.ReadLine();
        }

        [Serializable]
        public class PersonWithCompany
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Company Company { get; set; }

            public PersonWithCompany()
            { }

            public PersonWithCompany(string name, int age, Company comp)
            {
                Name = name;
                Age = age;
                Company = comp;
            }
        }

        [Serializable]
        public class Company
        {
            public string Name { get; set; }

            // стандартный конструктор без параметров
            public Company() { }

            public Company(string name)
            {
                Name = name;
            }
        }

        public static void Run2()
        {
            var person1 = new PersonWithCompany("Tom", 29, new Company("Microsoft"));
            var person2 = new PersonWithCompany("Bill", 25, new Company("Apple"));
            var people = new[] { person1, person2 };

            XmlSerializer formatter = new XmlSerializer(typeof(PersonWithCompany[]));

            using (FileStream fs = new FileStream("people-with-compny.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, people);
            }

            using (FileStream fs = new FileStream("people-with-compny.xml", FileMode.OpenOrCreate))
            {
                PersonWithCompany[] newPeople = (PersonWithCompany[])formatter.Deserialize(fs);

                foreach (PersonWithCompany p in newPeople)
                {
                    Console.WriteLine($"Имя: {p.Name} --- Возраст: {p.Age} --- Компания: {p.Company.Name}");
                }
            }
            Console.ReadLine();
        }
    }
}
