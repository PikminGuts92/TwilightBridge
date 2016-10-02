using System;
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
                        // Runs search and outputs result
                        Result result = sim.Run();
                        OutputResult(result);
                        break;
                }


            } while (c != 'x' && c != 'X');
        }

        static void OutputResult(Result result)
        {
            Console.WriteLine("=======================");
            Console.Write("Search Type: ");

            // Outputs search type
            switch (result.SearchType)
            {
                case Search.BreadthFirst:
                    Console.WriteLine("Breadth-First");
                    break;
                case Search.DepthFirst:
                    Console.WriteLine("Depth-First");
                    break;
                case Search.UniformCost:
                    Console.WriteLine("Uniform Cost");
                    break;
                default:
                    Console.WriteLine(result.SearchType);
                    break;
            }

            Console.WriteLine("Total Moves: {0}", result.WinPath.TotalMoves);
            Console.WriteLine("Shortest Path: {0}", result.WinPath.TotalCost);
            Console.WriteLine("Compute Time: {0}", result.ComputeTime);

            Console.WriteLine("=======================");
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
            char c;

            // Displays search types to user
            Console.WriteLine();
            Console.WriteLine("Search Types:");
            Console.WriteLine("\t\'b\' - Breadth-First");
            Console.WriteLine("\t\'d\' - Depth-First");
            Console.WriteLine("\t\'u\' - Uniform Cost");
            Console.WriteLine();

            do
            {
                // Gets search input from user
                Console.Write("Enter search type: ");
                string input = Console.ReadLine();

                // Converts string to char
                if (!Char.TryParse(input, out c))
                    c = '0';

                switch (c)
                {
                    case 'b':
                    case 'B':
                        // Sets BFS
                        sim.SetSearch(Search.BreadthFirst);
                        Console.WriteLine("Breadth-First search set!");
                        break;
                    case 'd':
                    case 'D':
                        // Sets DFS
                        sim.SetSearch(Search.DepthFirst);
                        Console.WriteLine("Depth-First search set!");
                        break;
                    case 'u':
                    case 'U':
                        // Sets UCS
                        sim.SetSearch(Search.UniformCost);
                        Console.WriteLine("Uniform Cost search set!");
                        break;
                    default:
                        c = '0';
                        break;
                }

            } while (c == '0');

        }
    }
}
