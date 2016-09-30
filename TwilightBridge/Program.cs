﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightBridge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Twilight Bridge v1.0 by Francisco Martinez");
            Bridge sim = new Bridge();

            // Input char
            char c;

            do
            {
                // Displays output to user
                Console.WriteLine();
                Console.WriteLine("Options:");
                Console.WriteLine("\t\'a\' - Set people");
                Console.WriteLine("\t\'b\' - Set search type");
                Console.WriteLine("\t\'r\' - Run");
                Console.WriteLine("\t\'x\' - Exit");
                Console.WriteLine();

                // Reads input
                Console.Write("Enter option: ");
                string input = Console.ReadLine();

                // Converts string to char
                if (!Char.TryParse(input, out c))
                    c = '0';

                switch(c)
                {
                    case 'a':
                    case 'A':
                        int count = InputCount();
                        int[] costs = new int[count];

                        for (int i = 0; i < costs.Length; i++)
                        {
                            costs[i] = InputCost(i);
                        }

                        // Sets costs
                        sim.SetCosts(costs);

                        break;
                    case 'b':
                    case 'B':
                        // Sets search type
                        InputSearch(sim);
                        break;
                    case 'c':
                    case 'C':
                        break;
                    case 'r':
                    case 'R':
                        sim.IterativeRun();
                        break;
                }


            } while (c != 'x' && c != 'X');
        }

        static int InputCount()
        {
            int count;

            do
            {
                // Gets count input from user
                Console.Write("Enter number of people: ");
                string input = Console.ReadLine();

                // Converts string to int
                if (!Int32.TryParse(input, out count))
                    count = 0;

            } while (count < 1);

            return count;
        }

        static int InputCost(int idx)
        {
            int cost;

            do
            {
                // Gets cost input from user
                Console.Write("Enter cost for person {0}: ", idx + 1);
                string input = Console.ReadLine();

                // Converts string to int
                if (!Int32.TryParse(input, out cost))
                    cost = -1;

            } while (cost < 0);

            return cost;
        }

        static void InputSearch(Bridge sim)
        {
            int searchNum;

            do
            {
                // Gets search input from user
                Console.Write("Enter search type: ");
                string input = Console.ReadLine();

                // Converts string to int
                if (!Int32.TryParse(input, out searchNum))
                    searchNum = -1;

                switch(searchNum)
                {
                    case 0:
                        sim.SetSearch(Search.BreadthFirst);
                        break;
                    case 1:
                        sim.SetSearch(Search.DepthFirst);
                        break;
                    case 2:
                        sim.SetSearch(Search.UniformCost);
                        break;
                }

            } while (searchNum < 0 || searchNum > 2);

        }
    }
}
