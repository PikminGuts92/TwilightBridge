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
            Bridge sim = new Bridge(4);
            //sim.RunLongTest();
            //sim.Run();
            sim.IterativeRun(Search.DepthFirst);
            Console.ReadKey();
        }

        static int InputCount()
        {
            return 3;
        }

        static int InputCost()
        {
            return 0;
        }
    }
}
