using SISE.Solution;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SISE
{
    class Program
    {

        static ISolver solver;
        static Stopwatch stopwatch;

        static void Main(string[] args)
        {

            var initializer = new Initializer(args);
            solver = initializer.Solver;
            stopwatch = Stopwatch.StartNew();
            string solutionString = solver.Solve();
            stopwatch.Stop();
            int solutionLength = solutionString != "No solution found!" ? solutionString.Length : -1;

        }
    }
}
