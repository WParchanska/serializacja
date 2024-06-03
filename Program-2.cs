using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace SerializationExample
{
    [Serializable]
    public class Osoba
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public DateTime DataUrodzenia { get; set; }
    }

    [Serializable]
    public class Student : Osoba
    {
        public string NumerIndeksu { get; set; }
        public string NumerGrupy { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Tworzenie obiektów klasy Osoba
            List<Osoba> osoby = new List<Osoba>
            {
                new Osoba { Imie = "Jan", Nazwisko = "Kowalski", Wiek = 30, DataUrodzenia = new DateTime(1992, 5, 15) },
                new Osoba { Imie = "Anna", Nazwisko = "Nowak", Wiek = 25, DataUrodzenia = new DateTime(1997, 10, 3) },
                new Osoba { Imie = "Adam", Nazwisko = "Wiśniewski", Wiek = 40, DataUrodzenia = new DateTime(1982, 8, 20) },
                new Osoba { Imie = "Justyna", Nazwisko = "Ptak", Wiek = 22, DataUrodzenia = new DateTime(2002, 3, 10) }
            };

            // Tworzenie obiektów klasy Student, które dziedziczą po klasie Osoba
            List<Student> studenci = new List<Student>
            {
                new Student { Imie = "Jan", Nazwisko = "Kowalski", Wiek = 30, DataUrodzenia = new DateTime(1992, 5, 15), NumerIndeksu = "123456", NumerGrupy = "A001" },
                new Student { Imie = "Anna", Nazwisko = "Nowak", Wiek = 25, DataUrodzenia = new DateTime(1997, 10, 3), NumerIndeksu = "654321", NumerGrupy = "B002" },
                new Student { Imie = "Adam", Nazwisko = "Wiśniewski", Wiek = 40, DataUrodzenia = new DateTime(1982, 8, 20), NumerIndeksu = "987654", NumerGrupy = "C003" },
                new Student { Imie = "Justyna", Nazwisko = "Ptak", Wiek = 22, DataUrodzenia = new DateTime(2002, 3, 10), NumerIndeksu = "456789", NumerGrupy = "D004" }
            };

            // Serializacja do XML
            SerializeToXml(osoby, "osoby.xml");
            SerializeToXml(studenci, "studenci.xml");

            // Serializacja do JSON
            SerializeToJson(osoby, "osoby.json");
            SerializeToJson(studenci, "studenci.json");

            // Deserializacja i wyświetlenie danych z plików XML
            var deserializowaneOsobyXml = DeserializeFromXml<List<Osoba>>("osoby.xml");
            var deserializowaniStudenciXml = DeserializeFromXml<List<Student>>("studenci.xml");
            Console.WriteLine("Deserializacja z XML:");
            Console.WriteLine("Osoby:");
            foreach (var osoba in deserializowaneOsobyXml)
            {
                Console.WriteLine($"{osoba.Imie} {osoba.Nazwisko}, Wiek: {osoba.Wiek}, Data Urodzenia: {osoba.DataUrodzenia.ToShortDateString()}");
            }
            Console.WriteLine("Studenci:");
            foreach (var student in deserializowaniStudenciXml)
            {
                Console.WriteLine($"{student.Imie} {student.Nazwisko}, Wiek: {student.Wiek}, Data Urodzenia: {student.DataUrodzenia.ToShortDateString()}, Numer Indeksu: {student.NumerIndeksu}, Numer Grupy: {student.NumerGrupy}");
            }

            // Deserializacja i wyświetlenie danych z plików JSON
            var deserializowaneOsobyJson = DeserializeFromJson<List<Osoba>>("osoby.json");
            var deserializowaniStudenciJson = DeserializeFromJson<List<Student>>("studenci.json");
            Console.WriteLine("\nDeserializacja z JSON:");
            Console.WriteLine("Osoby:");
            foreach (var osoba in deserializowaneOsobyJson)
            {
                Console.WriteLine($"{osoba.Imie} {osoba.Nazwisko}, Wiek: {osoba.Wiek}, Data Urodzenia: {osoba.DataUrodzenia.ToShortDateString()}");
            }
            Console.WriteLine("Studenci:");
            foreach (var student in deserializowaniStudenciJson)
            {
                Console.WriteLine($"{student.Imie} {student.Nazwisko}, Wiek: {student.Wiek}, Data Urodzenia: {student.DataUrodzenia.ToShortDateString()}, Numer Indeksu: {student.NumerIndeksu}, Numer Grupy: {student.NumerGrupy}");
            }
        }

        static void SerializeToXml<T>(T obj, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, obj);
            }
        }

        static void SerializeToJson<T>(T obj, string fileName)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (Stream stream = File.Create(fileName))
            {
                serializer.WriteObject(stream, obj);
            }
        }

        static T DeserializeFromXml<T>(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StreamReader(fileName))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        static T DeserializeFromJson<T>(string fileName)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (Stream stream = File.OpenRead(fileName))
            {
                return (T)serializer.ReadObject(stream);
            }
        }
    }
}
