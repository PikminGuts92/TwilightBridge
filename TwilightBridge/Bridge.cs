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
        private State _winState;
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
        
        public void IterativeRun(Search search)
        {
            _winCount = 0;
            _winState = null;

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

                    if (_winState == null || _winState.TotalCost > current.TotalCost)
                        _winState = current;

                    if (search == Search.UniformCost)
                        break; // Should be best route?
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
                            // Adds moves to fringe
                            fringe.AddRange(moves);

                            // Sorts list by total cost
                            fringe.Sort(delegate (State a, State b)
                            {
                                return a.TotalCost.CompareTo(b.TotalCost);
                            });

                            break;
                    }
                }
            }

            Console.WriteLine("Out of {0} paths, shortest path is {1} minutes in {2} steps", _winCount, _winState.TotalCost, _winState.TotalMoves);
        }
        
    }
}
