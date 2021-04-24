﻿using System;
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
                Console.WriteLine("  1 - Park list");
                Console.WriteLine("  2 - Add park");
                Console.WriteLine("  3 - Quit\n");
                optionSelect = Console.ReadKey();
                Console.WriteLine("\n\n");

                //Pressing 3 breaks loop and closes program
                if (optionSelect.KeyChar == '1')
                {
                    //Output 1
                    Console.Clear();
                    Console.WriteLine("  List of National Parks:");
                    Console.WriteLine("  ───────────────────────\n");
                    Output1();
                }
                else if (optionSelect.KeyChar == '2')
                {
                    //Output 2
                    Console.Clear();
                    Console.WriteLine("  Add National Park to Directory:");
                    Console.WriteLine("  ───────────────────────────────\n");
                    Output2();
                }
                else if (optionSelect.KeyChar == '3')
                {
                    //Output 2
                    Console.Clear();
                    Console.WriteLine("  Delete National Park from Directory:");
                    Console.WriteLine("  ────────────────────────────────────\n");
                    Output3();
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
            //Allows user to delete park.
        }
    }
}