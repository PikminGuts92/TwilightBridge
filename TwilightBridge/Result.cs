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
        private readonly long _computeTime; // In milliseconds
        private readonly int _numPeople;
        private readonly long _fringeItr;

        public Result(State winState, Search searchType, long computeTime, int numPeople, long fringeItr)
        {
            _winState = winState;
            _searchType = searchType;
            _computeTime = computeTime;
            _numPeople = numPeople;
            _fringeItr = fringeItr;
        }

        public State WinPath { get { return _winState; } }
        public Search SearchType { get { return _searchType; } }
        public long ComputeTime { get { return _computeTime; } }
        public int NumberOfPeople { get { return _numPeople; } }
        public long FringeIterations { get { return _fringeItr; } }
    }
}
