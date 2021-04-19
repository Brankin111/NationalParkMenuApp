using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NationalParksMenuApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool loopFlag = true;
            ConsoleKeyInfo optionSelect;

            //Menu loop
            while (loopFlag)
            {
                Console.Clear();
                Console.WriteLine("      National Parks Directory\n");
                Console.WriteLine("  ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
                Console.WriteLine("  Press select an option:\n");
                Console.WriteLine("  1 - List of national parks.");
                Console.WriteLine("  2 - Add park to directory.");
                Console.WriteLine("  3 - Delete park from directory.\n");
                Console.WriteLine("  Press Q to quit.");
                optionSelect = Console.ReadKey();
                Console.WriteLine("\n\n");

                switch (optionSelect.KeyChar)
                {
                    case 'q':
                    case 'Q':
                        break;

                    case '1':
                        break;

                    case '2':
                        break;

                    case '3':
                        break;
                }

                //Pressing Q breaks loop and closes program
                if ((optionSelect.KeyChar == 'q') || (optionSelect.KeyChar == 'Q'))
                {
                    loopFlag = false;
                }
                else if (optionSelect.KeyChar == '1')
                {
                    //Output 1.
                    Console.Clear();
                    Console.WriteLine("  List of National Parks:");
                    Console.WriteLine("  ───────────────────────\n");
                    Output1();
                }
                else if (optionSelect.KeyChar == '2')
                {
                    //Output 2.
                    Console.Clear();
                    Console.WriteLine("  Add National Park to Directory:");
                    Console.WriteLine("  ───────────────────────────────\n");
                    Output2();
                }
                else if (optionSelect.KeyChar == '3')
                {
                    //Output 3.
                    Console.Clear();
                    Console.WriteLine("  Delete National Park from Directory:");
                    Console.WriteLine("  ────────────────────────────────────\n");
                    Output3();
                }
                else
                {
                    //Output 4.
                    Console.Clear();
                    Console.WriteLine("  Please select a valid option.");
                }

                //Wait for user to press ENTER
                Console.WriteLine("  ────────────────────────────\n");
                Console.WriteLine("    Press ENTER to continue.");
                Console.ReadLine();
            }
        }

        private static void Output1()
        {
            string line;

            //Opens path to parks list txt file
            StreamReader sr = new StreamReader("F:\\Development\\NationalParksMenuApp\\ParkData.txt");
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
        }

        private static void Output2()
        {
            string line;

            //Allows user to add new park.
            using (StreamWriter file = new StreamWriter(@"F:\\Development\\NationalParksMenuApp\\ParkData.txt", true))
            {
                line = Console.ReadLine();
                file.WriteLine("  " + line + "\n");
            }
        }

        private static void Output3()
        {
            //Allows user to delete parks.
        }
    }
}