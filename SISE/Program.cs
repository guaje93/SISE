using SISE.Helpers;
using SISE.Solution;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SISE
{
    class Program
    {

        static ISolver solver;

        static void Main(string[] args)
        {

            var initializer = new Initializer(args);
            var resultGenerator = new ResultGenerators();
            solver = initializer.Solver;
            Stopwatch stopwatch = Stopwatch.StartNew();
            string solutionString = solver.Solve();
            stopwatch.Stop();
            int solutionLength = solutionString != "No solution found!" ? solutionString.Length : -1;
            resultGenerator.WriteResultState(initializer.SolutionFileDestination, solutionString);
            resultGenerator.WriteAdditionalInformation(initializer.SolutionInformationDestination,
                                                       solutionLength,
                                                       solver.NumberOfVisitedStates,
                                                       solver.NumberOfProcessedStates,
                                                       solver.MaxDepth,
                                                       stopwatch.ElapsedMilliseconds);

        }
    }
}
