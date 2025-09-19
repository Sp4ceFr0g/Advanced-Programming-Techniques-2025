using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lab2
{
    class City
    {
        public string Name { get; set; }
        public int Population { get; set; }

        public City(string name, int population)
        {
            Name = name;
            Population = population;
        }
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CityName { get; set; }
        public int Height { get; set; }
        public string Allergies { get; set; }

        public Person(string firstName, string lastName, string cityName, int height, string allergies)
        {
            FirstName = firstName;
            LastName = lastName;
            CityName = cityName;
            Height = height;
            Allergies = allergies;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = {106,104,10,5,117,174,95,61,74,145,77,95,72,59,114,95,61,116,106,66,75,85,104,62,76,87,70,17,141,39,199,91,37,139,88,84,15,166,118,54,42,123,53,183,95,101,112,26,41,135,70,48,59,69,109,93,110,153,178,117,5};

            City[] cities = {
                new City("Toronto", 100200),
                new City("Hamilton", 80923),
                new City("Ancaster", 4039),
                new City("Brantford", 500890),
            };

            Person[] persons = {
                new Person("Cedric","Coltrane","Toronto",157,null),
                new Person("Hank","Spencer","Peterborough",158,"Sulfa, Penicillin"),
                new Person("Sara","di","29",145,null),
                new Person("Daphne ","Seabright","Ancaster",146,null),
                new Person("Rick","Bennett","Ancaster",220,null),
                new Person("Amy","Leela","Hamilton",172,"Penicillin"),
                new Person("Woody","Bashir","Barrie",153,null),
                new Person("Tom", "Halliwell","Hamilton",179,"Codeine, Sulfa"),
                new Person("Rachel ","Winterbourne","Hamilton",163,null),
                new Person("John","West","Oakville",138,null),
                new Person("Jon","Doggett","Hamilton",194,"Peanut Oil"),
                new Person("Angel","Edwards","Brantford",176,null),
                new Person("Brodie","Beck","Carlisle",157,null),
                new Person("Beanie","Foster","Ancaster",154,"Ragweed, Codeine"),
                new Person("Nino","Andrews","Hamilton",186,null),
                new Person("John","Farley","Hamilton",213,null),
                new Person("Nea","Kobayakawa","Toronto",147,null),
                new Person("Laura","Halliwell","Brantford",146,null),
                new Person("Lucille","Maureen","Hamilton",184,null),
                new Person("Jim","Thoma","Ottawa",173,null),
                new Person("Roderick","Payne","Halifax",58,null),
                new Person("Sam","Threep","Hamilton",199,null),
                new Person("Bertha","Crowley","Delhi",125,"Peanuts, Gluten"),
                new Person("Roland","Edge","Brantford",199,null),
                new Person("Don","Wiggum","Hamilton",189,null),
                new Person("Anthony","Maxwell","Oakville",92,null),
                new Person("James","Sullivan","Delhi",139,null),
                new Person("Anne","Marlowe","Pickering",165,"Peanut Oil"),
                new Person("Kelly","Hamilton","Stoney",84,null),
                new Person("Charles","Andonuts","Hamilton",62,null),
                new Person("Temple ","Russert","Hamilton",166,"Sulphur"),
                new Person("Don","Edwards","Hamilton",215,null),
                new Person("Alice","Donovan","Hamilton",167,null),
                new Person("Stone","Cutting","Hamilton",110,null),
                new Person("Neil","Allan","Cambridge",203,null),
                new Person("Cross","Gordon","Ancaster",125,null),
                new Person("Phoebe","Bigelow","Thunder",183,null),
                new Person("Harry","Kuramitsu","Hamilton",210, null)
            };

            // 1a. Numbers > 80 (query syntax)
            var greaterThan80Query = from n in numbers where n > 80 select n;
            Console.WriteLine("1a Query: " + string.Join(", ", greaterThan80Query));

            // 1a. Numbers > 80 (method syntax)
            var greaterThan80Method = numbers.Where(n => n > 80);
            Console.WriteLine("1a Method: " + string.Join(", ", greaterThan80Method));

            // 1b. Order descending (query syntax)
            var descendingQuery = from n in numbers orderby n descending select n;
            Console.WriteLine("1b Query: " + string.Join(", ", descendingQuery));

            // 1b. Order descending (method syntax)
            var descendingMethod = numbers.OrderByDescending(n => n);
            Console.WriteLine("1b Method: " + string.Join(", ", descendingMethod));

            // 1c. Transform to string (query syntax)
            var transformQuery = from n in numbers select $"Have number #{n}";
            Console.WriteLine("1c Query: " + string.Join(", ", transformQuery));

            // 1c. Transform to string (method syntax)
            var transformMethod = numbers.Select(n => $"Have number #{n}");
            Console.WriteLine("1c Method: " + string.Join(", ", transformMethod));

            // 1d. Count 70 < n < 100
            var countQuery = from n in numbers where n < 100 && n > 70 select n;
            int count = countQuery.Count();
            Console.WriteLine("1d Count: " + count);

            // 2a. Persons with height (function, query syntax)
            static IEnumerable<Person> SelectByHeightQuery(Person[] p, int h) => from per in p where per.Height == h select per;
            Console.WriteLine("2a Query (height 157): " + string.Join(", ", SelectByHeightQuery(persons, 157).Select(p => p.FirstName)));

            // 2a. Method syntax
            static IEnumerable<Person> SelectByHeightMethod(Person[] p, int h) => p.Where(per => per.Height == h);
            Console.WriteLine("2a Method (height 157): " + string.Join(", ", SelectByHeightMethod(persons, 157).Select(p => p.FirstName)));

            // 2b. Transform name (query syntax)
            var nameTransformQuery = from p in persons select $"{p.FirstName[0]}. {p.LastName}";
            Console.WriteLine("2b Query: " + string.Join(", ", nameTransformQuery));

            // 2b. Method syntax
            var nameTransformMethod = persons.Select(p => $"{p.FirstName[0]}. {p.LastName}");
            Console.WriteLine("2b Method: " + string.Join(", ", nameTransformMethod));

            // 2c. Distinct allergies (query syntax)
            var allergiesQuery = (from p in persons where p.Allergies != null from a in p.Allergies.Split(", ") select a).Distinct();
            Console.WriteLine("2c Query: " + string.Join(", ", allergiesQuery));

            // 2c. Method syntax
            var allergiesMethod = persons.Where(p => p.Allergies != null).SelectMany(p => p.Allergies.Split(", ")).Distinct();
            Console.WriteLine("2c Method: " + string.Join(", ", allergiesMethod));

            // 2d. Cities starting with H
            var hCities = (from c in cities where c.Name.StartsWith("H") select c).Count();
            Console.WriteLine("2d: " + hCities);

            // 2e. Join persons and cities, population > 100000
            var largeCitiesPersons = from p in persons join c in cities on p.CityName equals c.Name where c.Population > 100000 select p.FirstName;
            Console.WriteLine("2e: " + string.Join(", ", largeCitiesPersons));

            // 2f. Manual list of cities
            List<string> specificCities = new List<string> { "Toronto", "Hamilton", "Ancaster" };

            // Persons in those cities
            var inCities = from p in persons where specificCities.Contains(p.CityName) select p.FirstName;
            Console.WriteLine("2f In: " + string.Join(", ", inCities));

            // Persons not in those cities
            var notInCities = from p in persons where !specificCities.Contains(p.CityName) select p.FirstName;
            Console.WriteLine("2f Not In: " + string.Join(", ", notInCities));

            // 3. Convert persons to XML
            XElement personsXml = new XElement("Persons",
                from p in persons
                select new XElement("Person",
                    new XAttribute("FirstName", p.FirstName),
                    new XAttribute("LastName", p.LastName),
                    new XAttribute("City", p.CityName),
                    new XAttribute("Height", p.Height),
                    new XAttribute("Allergies", p.Allergies ?? "")
                )
            );
            Console.WriteLine("3. XML:\n" + personsXml);

            Console.ReadKey();
        }
    }
}