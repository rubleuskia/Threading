using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JSON
{
    public class JsonService
    {
        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public static void Run1()
        {
            Person tom = new Person { Name = "Tom", Age = 35 };
            string json = JsonSerializer.Serialize<Person>(tom);
            Console.WriteLine(json);
            Person restoredPerson = JsonSerializer.Deserialize<Person>(json);
            Console.WriteLine(restoredPerson.Name);
        }

        public static async Task Run2()
        {
            // сохранение данных
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                Person tom = new Person { Name = "Tom", Age = 35 };
                await JsonSerializer.SerializeAsync<Person>(fs, tom);
                Console.WriteLine("Data has been saved to file");
            }

            // чтение данных
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
                Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
            }
        }

        public static void Run3()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = false,

                // ...
            };

            Person tom = new Person { Name = "Tom", Age = 35 };
            string json = JsonSerializer.Serialize<Person>(tom, options);
            Console.WriteLine(json);
            Person restoredPerson = JsonSerializer.Deserialize<Person>(json);
            Console.WriteLine(restoredPerson.Name);
        }

        class CustomPerson
        {
            [JsonPropertyName("firstname")]
            public string Name { get; set; }

            [JsonIgnore]
            public int Age { get; set; }
        }

        public static void Run4()
        {
            CustomPerson tom = new CustomPerson { Name = "Tom", Age = 35 };
            string json = JsonSerializer.Serialize<CustomPerson>(tom);
            Console.WriteLine(json);
            CustomPerson restoredPerson = JsonSerializer.Deserialize<CustomPerson>(json);
            Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
        }

        // https://www.newtonsoft.com/json
    }
}
