using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightBridge
{
    public class Bridge
    {
        HashSet<ulong> _states;
        private ulong _start;
        private ulong _end;
        private int[] _costs;

        public Bridge(int numCost)
        {
            _start = 0;
            _end = 1;
            _costs = new int[numCost];

            ulong endCost = 1;

            for (int i = 0; i < numCost; i++)
            {
                endCost = endCost << 1;
                _end = _end + endCost;

                // Gets cost input from user
                Console.WriteLine("Enter cost for person {0}: ", i + 1);
                string input = Console.ReadLine();

                _costs[i] = Convert.ToInt32(input);
            }
        }

        public void RunLongTest()
        {
            // Resets known states
            _states = new HashSet<ulong>();
            _states.Add(_start);
            
            HashSet<ulong> path = new HashSet<ulong>();
            path.Add(_start);

            List<ulong> fringe = new List<ulong>();
            fringe = Expand(_start, path);

            int success = 0;

            while (fringe.Count > 0)
            {
                // Successful path found
                if (fringe[0] == _end)
                {
                    success++;
                    path.Clear();
                }

                List<ulong> nextStates = Expand(fringe[0], path);
                fringe.RemoveAt(0);
                fringe.AddRange(nextStates);

                //break;
            }

            Console.WriteLine("Finished!");
        }

        private List<ulong> Expand(ulong state, HashSet<ulong> path)
        {
            List<ulong> children = new List<ulong>();

            ulong currentPlace = 1;
            ulong nextState = state & ~currentPlace;

            bool choose2 = false;

            // 0 = Choose 2, 1 = Choose 1
            if ((currentPlace & state) == 0)
                choose2 = true;

            if (choose2) // Point A -> Point B
            {
                for (int i = 0; i < _costs.Length; i++)
                {
                    currentPlace = currentPlace << 1;
                    ulong canCross = nextState & currentPlace;

                    // Can cross
                    if (canCross == 0)
                    {
                        children.Add(currentPlace + nextState);
                    }
                }

                List<ulong> subChildren = new List<ulong>();

                // The choose 2 function
                for (int i = 0; i < children.Count - 1; i++)
                {
                    for (int j = i + 1; j < children.Count; j++)
                    {
                        ulong newState = (children[i] | children[j]) + 1;

                        // State is added to returned children if state is actually new
                        if (path.Add(newState))
                            subChildren.Add(newState);
                    }
                }

                return subChildren;
            }
            else // Point A <- Point B
            {
                for (int i = 0; i < _costs.Length; i++)
                {
                    currentPlace = currentPlace << 1;
                    ulong canCross = nextState & currentPlace;

                    // Can cross back
                    if (canCross != 0)
                    {
                        ulong newState = ~canCross & nextState;

                        // State is added to returned children if state is actually new
                        if (path.Add(newState))
                            children.Add(newState);
                    }
                }
            }

            // Returns all expanded states
            return children;
        }
    }
}
