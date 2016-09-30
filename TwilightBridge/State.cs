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
        
        private State()
        {

        }

        public State(ulong state)
        {
            _state = state;
            _path = new HashSet<ulong>();
            _path.Add(state);
        }

        public static bool operator==(State a, State b)
        {
            return a._state == b._state;
        }

        public static bool operator !=(State a, State b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is State)) return false;

            return ((State)obj)._state == _state;
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

        public static int[] Cost { get { return _costs; } set { _costs = value; } }

        public ulong Value { get { return _state; } }
    }
}
