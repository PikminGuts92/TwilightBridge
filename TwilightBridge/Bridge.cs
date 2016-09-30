using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightBridge
{
    public class Bridge
    {
        private ulong _start;
        private ulong _end;
        private int[] _costs;
        private List<ulong> _winPath;
        private int _winCost;
        private int _winCount;

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

        public void Run()
        {
            _winCount = 0;
            _winCost = 0;
            HashSet<ulong> path = new HashSet<ulong>();

            RecursiveRun(_start, path);

            //Console.WriteLine("\r\nFinished!");

            List<ulong> quickestPath = _winPath;
            int smallestCost = _winCost;

            Console.WriteLine("Out of {0} paths, shortest path is {1} minutes in {2} steps", _winCount, smallestCost, quickestPath.Count);
        }

        private bool SetWinPath(List<ulong> path)
        {
            int cost = GetTotalCost(path);

            if (_winPath == null)
            {
                _winPath = path;
                _winCost = cost;
                return true;
            }

            if (cost < _winCost)
            {
                _winPath = path;
                _winCost = cost;
                return true;
            }
            else
                return false;
        }

        public void IterativeRun(Search search)
        {
            _winCount = 0;

            // Sets costs
            State.Cost = _costs;

            // Creates fringe
            List<State> fringe = new List<State>();
            fringe.Add(new State(_start));

            while (fringe.Count > 0)
            {
                State current = fringe.First();
                fringe.RemoveAt(0);

                if (current.Value == _end)
                {
                    // Goal reached
                    _winCount++;
                }
                else
                {
                    // Gets possible moves
                    List<State> moves = current.Expand();

                    switch (search)
                    {
                        case Search.BreadthFirst:
                            // Adds moves to end of fringe
                            fringe.AddRange(moves);
                            break;
                        case Search.DepthFirst:
                            // Adds moves to beginning of fringe
                            fringe.InsertRange(0, moves);
                            break;
                        case Search.UniformCost:
                            break;
                    }
                }
            }

            Console.WriteLine("Out of {0} paths, shortest path is {0} minutes in {0} steps", _winCount);
        }

        private void RecursiveRun(ulong state, HashSet<ulong> path)
        {
            // Adds state to hash set
            path.Add(state);

            if (state == _end)
            {
                // Goal reached
                //_winPaths.Add(new List<ulong>(path));
                SetWinPath(new List<ulong>(path));
                _winCount++;
            }
            else
            {
                // Expands state
                List<ulong> children = Expand(state, path);

                foreach (ulong child in children)
                {
                    RecursiveRun(child, path);
                }
            }

            // Removes state from hash set
            path.Remove(state);
        }

        private int GetTotalCost(List<ulong> states)
        {
            int totalCost = 0;

            ulong previousState = states[0];

            for (int i = 1; i < states.Count; i++)
            {
                previousState = previousState ^ states[i];

                ulong currentBit = 1;
                int stepCost = 0;

                for (int j = 0; j < _costs.Length; j++)
                {
                    currentBit = currentBit << 1;

                    if ((currentBit & previousState) != 0 && _costs[j] > stepCost)
                        stepCost = _costs[j];
                }

                previousState = states[i];
                totalCost += stepCost;
            }

            return totalCost;
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
                        if (!path.Contains(newState))
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
                        if (!path.Contains(newState))
                            children.Add(newState);
                    }
                }
            }

            // Returns all expanded states
            return children;
        }
    }
}
