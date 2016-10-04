using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightBridge
{
    public class State
    {
        private static int[] _costs; // Globally used
        private ulong _state;
        HashSet<ulong> _path;

        private int _totalCost;
        private int _moveCost;
        
        private State()
        {

        }

        public State(ulong state)
        {
            _state = state;
            _path = new HashSet<ulong>();
            _path.Add(state);

            _totalCost = 0;
            _moveCost = 0;
        }
        
        private int CalculateCost(ulong state1, ulong state2)
        {
            int moveCost = 0;

            ulong difference = state1 ^ state2;
            ulong currentBit = 1;

            if ((difference & currentBit) == 0)
                return moveCost; // State didn't alternate (Torch on same side)
            
            for (int j = 0; j < _costs.Length; j++)
            {
                currentBit = currentBit << 1;

                if ((currentBit & difference) != 0 && _costs[j] > moveCost)
                    moveCost = _costs[j];
            }
            
            return moveCost;
        }

        public List<State> Expand()
        {
            // Gets all possible int64 move states
            List<ulong> _children = Expand(_state, _path);
            List<State> _childrenStates = new List<State>();
            
            foreach(ulong child in _children)
            {
                // Sets int64 state
                State state = new State();
                state._state = child;

                // Sets path
                state._path = new HashSet<ulong>(_path);
                state._path.Add(child);

                // Calculates move cost
                state._moveCost = CalculateCost(_path.Last(), child);
                state._totalCost = _totalCost + state._moveCost;

                // Adds state
                _childrenStates.Add(state);
            }

            return _childrenStates;
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

        public void DisplayPath()
        {
            string[] costNames = new string[_costs.Length + 1];
            int lengthCostNames = 0;

            // Generates unique names
            for (int i = 0; i < costNames.Length; i++)
            {
                if (i == 0) // Torch
                    costNames[i] = "*";
                else
                    costNames[i] = GetName(i - 1);

                lengthCostNames += costNames[i].Length;
            }

            Console.WriteLine("Cost {0}       Right", "Left".PadRight(lengthCostNames + costNames.Length));

            ulong previousState = _path.First();
            int runningCost = 0;

            foreach(ulong state in _path)
            {
                string leftSide = "";
                string rightSide = "";

                ulong currentBit = 1;

                for (int i = 0; i <= _costs.Length; i++)
                {
                    if ((currentBit & state) == 0)
                    {
                        // 0 = left side, else right side
                        leftSide += costNames[i] + " ";
                        rightSide += new string(' ', costNames[i].Length + 1);
                    }
                    else
                    {
                        leftSide += new string(' ', costNames[i].Length + 1);
                        rightSide += costNames[i] + " ";
                    }

                    currentBit = currentBit << 1;
                }

                runningCost += CalculateCost(previousState, state);

                Console.WriteLine("{0} {1}------ {2}", runningCost.ToString().PadLeft(4, ' '), leftSide, rightSide);
                previousState = state;
            }
        }

        private string GetName(int idx)
        {
            string name = "";

            // Generates unique name for idx
            while (idx > -1)
            {
                int ch = idx % 26;
                
                name += Convert.ToChar('a' + ch);
                idx = idx - 26;
            }

            return name;
        }

        public static int[] Cost { get { return _costs; } set { _costs = value; } }

        public ulong Value { get { return _state; } }

        public int TotalCost { get { return _totalCost; } }
        public int MoveCost { get { return _moveCost; } }

        public int TotalMoves { get { return _path.Count - 1; } }
    }
}
