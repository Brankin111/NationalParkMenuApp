using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace NationalParksMenuApp
{
    internal class Program
    {
        private const string PARKS_LIST_PATH = "NationalParks.json";
        private static List<NationalPark> _parks;

        private static void Main(string[] args)
        {
            bool loopFlag = true;
            ConsoleKeyInfo optionSelect;

            //Reads park as soon as the program runs
            GetParksData();

            //Menu loop
            while (loopFlag)
            {
                Console.Clear();
                Console.WriteLine("      National Parks Directory\n");
                Console.WriteLine("  ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
                Console.WriteLine("  Press select an option:\n");
                Console.WriteLine("  1 - Park list");
                Console.WriteLine("  2 - Add park");
                Console.WriteLine("  3 - Delete park");
                Console.WriteLine("  4 - Quit\n");
                optionSelect = Console.ReadKey();
                Console.WriteLine("\n\n");

                //User key input determines menu selection
                if (optionSelect.KeyChar == '1')
                {
                    Console.Clear();
                    Console.WriteLine("  List of National Parks:");
                    Console.WriteLine("  ───────────────────────\n");
                    OutputAllParks();
                }
                else if (optionSelect.KeyChar == '2')
                {
                    Console.Clear();
                    Console.WriteLine("  Add National Park to Directory:");
                    Console.WriteLine("  ───────────────────────────────\n");
                    AddNewPark();
                }
                else if (optionSelect.KeyChar == '3')
                {
                    Console.Clear();
                    Console.WriteLine("  Delete National Park from Directory:");
                    Console.WriteLine("  ────────────────────────────────────\n");
                    DeletePark();
                }
                else if (optionSelect.KeyChar == '4')
                {
                    loopFlag = false;
                }
                else
                {
                    //Output if invalid
                    Console.Clear();
                    Console.WriteLine("  Please select a valid option.");
                }

                //Wait for user to press ENTER
                Console.WriteLine("  ────────────────────────────\n");
                Console.WriteLine("    Press ENTER to continue.");
                Console.ReadLine();
            }
        }

        //Outputs name and locaion of each park
        private static void OutputAllParks()
        {
            foreach (var park in _parks)
            {
                //write the line to console window
                Console.WriteLine($"{park.State}, {park.LocationName}");
                Console.WriteLine();
            }
        }

        //Allows user to add new park.
        private static void AddNewPark()
        {
            using (var file = new StreamWriter(PARKS_LIST_PATH))
            {
                Console.WriteLine(" State: ");
                var state = Console.ReadLine();

                Console.WriteLine();

                Console.WriteLine(" Park Name: ");
                var parkName = Console.ReadLine();

                var newPark = new NationalPark();
                newPark.State = state;
                newPark.LocationName = parkName;

                _parks.Add(newPark);

                file.Write(JsonConvert.SerializeObject(_parks));
            }
        }

        private static void DeletePark()
        {
            //Allows user to delete park.
            //Still researching....
        }

        /// <summary>
        /// Loads National Parks data from a JSON file.
        /// </summary>
        private static void GetParksData()
        {
            using (var sr = new StreamReader(PARKS_LIST_PATH))
            {
                _parks = JsonConvert.DeserializeObject<List<NationalPark>>(sr.ReadToEnd());
            }
        }
    }
}