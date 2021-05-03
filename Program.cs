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
        private static bool loopFlag = true;

        private static void Main(string[] args)
        {
            //Menu loop
            while (loopFlag)
            {
                LoadMainMenu();
            }
        }

        private static void LoadMainMenu()
        {
            ConsoleKeyInfo optionSelect;
            SetParksData();

            //Menu header and options
            Console.Clear();
            Console.WriteLine("      National Parks Directory\n");
            Console.WriteLine("  ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
            Console.WriteLine("  Press select an option:\n");
            Console.WriteLine("  1 - View List of Parks");
            Console.WriteLine("  2 - Add Park");
            Console.WriteLine("  3 - Delete Park");
            Console.WriteLine("  4 - Edit Park");
            Console.WriteLine("  5 - Quit\n");
            optionSelect = Console.ReadKey();
            Console.WriteLine("\n\n");

            //User key input determines menu selection
            if (optionSelect.KeyChar == '1')
            {
                Console.Clear();
                Console.WriteLine("  List of National Parks:");
                Console.WriteLine("  ───────────────────────\n");
                OutputAllParks(false);
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
                Console.Clear();
                Console.WriteLine("  Edit National Park in Directory:");
                Console.WriteLine("  ────────────────────────────────\n");
                LoadEditParkMenu();
            }
            else if (optionSelect.KeyChar == '5')
            {
                loopFlag = false;
            }
            else
            {
                //Output if invalid
                Console.Clear();
                Console.WriteLine("  Please select a valid option.");
            }
        }

        private static void AddNewPark()
        {
            Console.WriteLine(" Enter new Park State Abbreviation: ");
            var state = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(state))
            {
                Console.WriteLine("Invalid state name. Hit ENTER to return to Main Menu.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine();

            Console.WriteLine(" Enter new Park Name: ");
            var parkName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(parkName))
            {
                Console.WriteLine("Invalid park name.Hit ENTER to return to Main Menu.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine();

            Console.WriteLine(" Enter new Park Location Number: ");
            var locNumber = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(locNumber))
            {
                Console.WriteLine("Invalid park location number.Hit ENTER to return to Main Menu.");
                Console.ReadLine();
                return;
            }

            var newPark = new NationalPark();
            newPark.State = state;
            newPark.LocationName = parkName;
            newPark.LocationNumber = locNumber;

            _parks.Add(newPark);

            WriteParksToFile();

            Console.WriteLine($"You successfully added a new park: '{newPark}'");
            Console.WriteLine("Hit ENTER to return to main menu");
            Console.ReadLine();
        }

        private static void DeletePark()
        {
            Console.WriteLine("Which park would you like to delete?");
            Console.WriteLine("1. Press 1 to enter Location Number of park to delete");
            Console.WriteLine("2. Press 2 to return to main menu");
            var userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case ('1'):
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid Park Location Number to delete a park.");
                    var locNumber = Console.ReadLine();
                    SetParksData();
                    var parkToDelete = GetParkByLocationNumber(locNumber);
                    if (parkToDelete == null)
                    {
                        Console.Clear();
                        Console.WriteLine($"'{locNumber}' is not a valid Park Location Number.");
                        Console.WriteLine();
                    }
                    else
                    {
                        _parks.Remove(parkToDelete);
                        WriteParksToFile();
                        Console.WriteLine($"You successfully deleted '{parkToDelete.LocationName}'.");
                    }
                    break;

                case ('2'):
                    // valid option, return control to main menu below
                    break;

                default:
                    ShowInvalidUserInputMessage(userChoice.KeyChar.ToString());
                    break;
            }

            if (userChoice.KeyChar != '2')
            {
                Console.WriteLine("Hit ENTER to return to main menu");
                Console.ReadLine();
            }
        }

        private static void LoadEditParkMenu()
        {
            Console.WriteLine("Which park would you like to edit?");
            Console.WriteLine("1. Press 1 to enter Location Number of park to edit");
            Console.WriteLine("2. Press 2 to return to the main menu");
            var userChoice = Console.ReadKey();
            var returnToMainWithoutPrompt = false;

            switch (userChoice.KeyChar)
            {
                case ('1'):
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid Park Location Number to edit a park.");
                    var locNumber = Console.ReadLine();
                    SetParksData();
                    var parkToEdit = GetParkByLocationNumber(locNumber);
                    if (parkToEdit == null)
                    {
                        Console.Clear();
                        Console.WriteLine($"'{locNumber}' is not a valid Park Location Number.");
                        Console.WriteLine();
                    }
                    else
                    {
                        returnToMainWithoutPrompt = EditPark(parkToEdit);
                    }
                    break;

                case ('2'):
                    // valid option, return control to main menu below
                    returnToMainWithoutPrompt = true;
                    break;

                default:
                    ShowInvalidUserInputMessage(userChoice.KeyChar.ToString());
                    break;
            }

            if (!returnToMainWithoutPrompt)
            {
                Console.WriteLine("Hit ENTER to return to main menu");
                Console.ReadLine();
            }
        }

        private static bool EditPark(NationalPark parkToEdit)
        {
            Console.WriteLine();
            Console.WriteLine("Which field would you like to edit?");
            Console.WriteLine("1. Press 1 to edit park name");
            Console.WriteLine("2. Press 2 to edit park state");
            Console.WriteLine("3. Press 3 to return to edit menu");
            Console.WriteLine("4. Press 4 to return to main menu");
            var userChoice = Console.ReadKey();
            Console.WriteLine();
            var returnToMainWithoutPrompt = false;

            switch (userChoice.KeyChar)
            {
                case ('1'):
                    Console.WriteLine($"Current park name: {parkToEdit.LocationName}");
                    Console.WriteLine("Enter new park name:");
                    var newParkName = Console.ReadLine();
                    var oldName = parkToEdit.LocationName;
                    parkToEdit.LocationName = newParkName;
                    WriteParksToFile();
                    Console.WriteLine($"You successfully renamed '{oldName}' to '{newParkName}'.");
                    break;

                case ('2'):
                    Console.WriteLine($"Current park state: {parkToEdit.State}");
                    Console.WriteLine("Enter new park state:");
                    var newParkState = Console.ReadLine();
                    var oldState = parkToEdit.State;
                    parkToEdit.State = newParkState;
                    WriteParksToFile();
                    Console.WriteLine($"You successfully changed the park state from '{oldState}' to '{newParkState}'.");
                    break;

                case ('3'):
                    Console.Clear();
                    LoadEditParkMenu();
                    break;

                case ('4'):
                    // valid option, return control to main menu below
                    returnToMainWithoutPrompt = true;
                    break;

                default:
                    ShowInvalidUserInputMessage(userChoice.KeyChar.ToString());
                    break;
            }

            return returnToMainWithoutPrompt;
        }

        private static void SetParksData()
        {
            using (var sr = new StreamReader(PARKS_LIST_PATH))
            {
                _parks = JsonConvert.DeserializeObject<List<NationalPark>>(sr.ReadToEnd());
            }
        }

        private static void WriteParksToFile()
        {
            using (var file = new StreamWriter(PARKS_LIST_PATH))
            {
                file.Write(JsonConvert.SerializeObject(_parks));
            }
        }

        private static void OutputAllParks(bool autoReturn = true)
        {
            foreach (var park in _parks)
            {
                Console.WriteLine(park);
                Console.WriteLine();
            }

            if (!autoReturn)
            {
                Console.WriteLine("Hit ENTER to continue");
                Console.ReadLine();
            }
        }

        private static void ShowInvalidUserInputMessage(string userInput)
        {
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                Console.Clear();
                Console.WriteLine($"'{userInput}' is not a valid option.  Please enter a valid option.");
                Console.WriteLine();
            }
        }

        private static NationalPark GetParkByLocationNumber(string locNumber)
        {
            NationalPark park = null;

            if (!string.IsNullOrWhiteSpace(locNumber))
            {
                park = _parks.Where(x => !string.IsNullOrWhiteSpace(x.LocationNumber) &&
                x.LocationNumber.Equals(locNumber, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            }
            return park;
        }
    }
}