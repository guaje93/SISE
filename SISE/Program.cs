using SISE.Helpers;
using SISE.Solution;
using System;
using System.Diagnostics;

namespace SISE
{
    class Program
    {
        #region Fields

        static ISolver solver;
        static Stopwatch stopwatch;
        static string solutionString;

        #endregion

        #region Methods


        static void Main(string[] args)
        {
            Initializer initializer = Initialize(args);
            RunSolver();
            SaveResult(initializer.SolutionFileDestination, initializer.SolutionInformationDestination);
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

        private static void RunSolver()
        {
            Console.WriteLine($"Solver {solver.GetType().Name}: start processing at {System.DateTime.Now.ToShortTimeString()}");
            stopwatch = Stopwatch.StartNew();
            solutionString = solver.Solve();
            stopwatch.Stop();
            Console.WriteLine($"Solver {solver.GetType().Name}: finished in {stopwatch.ElapsedMilliseconds}ms");
        }

        private static void SaveResult(string solutionFileDestination, string solutionInformationDestination)
        {
            var resultGenerator = new ResultGenerators();
            int solutionLength = solutionString != "No solution found!" ? solutionString.Length : -1;
            var resultSaved = resultGenerator.WriteSolution(solutionFileDestination, solutionString);
            var additionalResultSaved = resultGenerator.WriteAdditionalInformation(solutionInformationDestination,
                                                       solutionLength,
                                                       solver.StatesVisitedAmount,
                                                       solver.StatesProcessedAmount,
                                                       solver.MaxDepth,
                                                       stopwatch.ElapsedMilliseconds);
            if (resultSaved && additionalResultSaved)
            {
                Console.WriteLine("Result saved");
                Console.WriteLine($"Additional information saved");
            }
        }

        #endregion
    }
}
