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
        private readonly long _computeTime; // Milliseconds
        private readonly long _computTimeTicks; // Ticks
        private readonly int _numPeople;
        private readonly long _fringeItr;

        /// <summary>
        /// Constructor for Result object
        /// </summary>
        /// <param name="winState">Win path</param>
        /// <param name="searchType">Search ysed</param>
        /// <param name="computeTime">Compute time (ms)</param>
        /// <param name="computeTimeTicks">Compute time (ticks)</param>
        /// <param name="numPeople">Number of people</param>
        /// <param name="fringeItr">Number of fringe iterations</param>
        public Result(State winState, Search searchType, long computeTime, long computeTimeTicks, int numPeople, long fringeItr)
        {
            _winState = winState;
            _searchType = searchType;
            _computeTime = computeTime;
            _computTimeTicks = computeTimeTicks;
            _numPeople = numPeople;
            _fringeItr = fringeItr;
        }

        /// <summary>
        /// Gets win path
        /// </summary>
        public State WinPath { get { return _winState; } }

        /// <summary>
        /// Gets search type
        /// </summary>
        public Search SearchType { get { return _searchType; } }

        /// <summary>
        /// Gets compute time (ms)
        /// </summary>
        public long ComputeTime { get { return _computeTime; } }

        /// <summary>
        /// Gets compute time (ticks)
        /// </summary>
        public long ComputeTimeTicks { get { return _computTimeTicks; } }

        /// <summary>
        /// Gets number of people
        /// </summary>
        public int NumberOfPeople { get { return _numPeople; } }

        /// <summary>
        /// Gets number of fringe iterations
        /// </summary>
        public long FringeIterations { get { return _fringeItr; } }
    }
}
