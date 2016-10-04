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
            bool costSet = false;

            // Input char
            char c;

            do
            {
                // Displays output to user
                Console.WriteLine();
                Console.WriteLine("Options:");

                Console.Write("\t\'a\' - Set people");

                // Appends # of people
                if (costSet)
                    Console.WriteLine(" (n = {0})", State.Cost.Length);
                else
                    Console.WriteLine(" (n = ?)");


                Console.Write("\t\'b\' - Set search type");

                // Appends search type
                switch(sim.SearchType)
                {
                    case Search.BreadthFirst:
                        Console.WriteLine(" (Breadth-First)");
                        break;
                    case Search.DepthFirst:
                        Console.WriteLine(" (Depth-First)");
                        break;
                    case Search.UniformCost:
                        Console.WriteLine(" (Unifrom Cost)");
                        break;
                }

                // Shows run only after cost is set
                if (costSet) Console.WriteLine("\t\'r\' - Run");

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
                        costSet = true;

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
                        if (costSet)
                        {
                            // Runs search and outputs result
                            Result result = sim.Run();
                            OutputResult(result);
                        }
                        break;
                }


            } while (c != 'x' && c != 'X');
        }

        static void OutputResult(Result result)
        {
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine("Number of People: {0}", result.NumberOfPeople);
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

            Console.WriteLine("Shortest Path: {0} mins in {1} moves", result.WinPath.TotalCost, result.WinPath.TotalMoves);
            Console.WriteLine("Fringe Iterations: {0}", result.FringeIterations);
            Console.WriteLine("Compute Time: {0} ticks ({1} ms)", result.ComputeTimeTicks, result.ComputeTime);
            Console.WriteLine();

            Console.Write("Costs = { ");

            int[] costs = State.Cost;

            // Writes out costs
            for (int i = 0; i < costs.Length; i++)
            {
                if (i == costs.Length - 1)
                    Console.Write("{0} ", costs[i]);
                else
                    Console.Write("{0}, ", costs[i]);
            }

            Console.WriteLine("}");
            Console.WriteLine();

            result.WinPath.DisplayPath();

            Console.WriteLine("=====================================");
        }

        static int InputCount()
        {
            int count;

            do
            {
                // Gets count input from user
                Console.Write("Enter number of people (n > 1): ");
                string input = Console.ReadLine();

                // Converts string to int
                if (!Int32.TryParse(input, out count))
                    count = 0;

            } while (count < 2);

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
                        sim.SearchType = Search.BreadthFirst;
                        Console.WriteLine("Breadth-First search set!");
                        break;
                    case 'd':
                    case 'D':
                        // Sets DFS
                        sim.SearchType = Search.DepthFirst;
                        Console.WriteLine("Depth-First search set!");
                        break;
                    case 'u':
                    case 'U':
                        // Sets UCS
                        sim.SearchType = Search.UniformCost;
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
