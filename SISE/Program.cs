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
        static Stopwatch stopwatch;
        static string solutionString;

        static void Main(string[] args)
        {
            Initializer initializer = Initialize(args);
            RunSolver();
            SaveResult(initializer.SolutionFileDestination, initializer.SolutionInformationDestination);
        }

        private static void SaveResult(string solutionFileDestination, string solutionInformationDestination)
        {
            var resultGenerator = new ResultGenerators();
            int solutionLength = solutionString != "No solution found!" ? solutionString.Length : -1;
            resultGenerator.WriteResultState(solutionFileDestination, solutionString);
            resultGenerator.WriteAdditionalInformation(solutionInformationDestination,
                                                       solutionLength,
                                                       solver.NumberOfVisitedStates,
                                                       solver.NumberOfProcessedStates,
                                                       solver.MaxDepth,
                                                       stopwatch.ElapsedMilliseconds);
        }

        private static void RunSolver()
        {
            stopwatch = Stopwatch.StartNew();
            solutionString = solver.Solve();
            stopwatch.Stop();
        }

        private static Initializer Initialize(string[] args)
        {
            if (!Initializer.Validate(args))
            {
                Environment.Exit(-1);
            }
            var initializer = new Initializer(args);
            solver = initializer.Solver;
            return initializer;
        }
    }
}
