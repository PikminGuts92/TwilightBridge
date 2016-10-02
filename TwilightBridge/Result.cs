using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightBridge
{
    public class Result
    {
        private readonly State _winState;
        private readonly Search _searchType;
        private readonly int _computeTime; // In milliseconds

        public Result(State winState, Search searchType, int computeTime)
        {
            _winState = winState;
            _searchType = searchType;
            _computeTime = computeTime;
        }

        public State WinPath { get { return _winState; } }
        public Search SearchType { get { return _searchType; } }
        public int ComputeTime { get { return _computeTime; } }
    }
}
