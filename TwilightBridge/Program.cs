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
            Bridge sim = new Bridge(7);
            //sim.RunLongTest();
            sim.Run();
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
