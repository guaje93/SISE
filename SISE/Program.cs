using SISE.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

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
<<<<<<< HEAD
            List<string[]> agumentsList = LoadArguments();
            foreach (string[] arguments in agumentsList)
            {
                Initializer initializer = Initialize(arguments);
                RunSolver();
                SaveResult(initializer.SolutionFileDestination, initializer.SolutionInformationDestination);
            }
=======
            List<string[]> argumentList = LoadArguments();
            foreach (string[] argument in argumentList)
            {
                Initializer initializer = Initialize(argument);
                RunSolver();
                SaveResult(initializer.SolutionFileDestination, initializer.SolutionInformationDestination);
            }
        }

        private static List<string[]> LoadArguments()
        {
            List<string[]> launchArgs = new List<string[]>();
            string[] strategies = { "bfs", "dfs", "astrM", "astrH" };
            string[] searchOrders = { "RDUL", "RDLU", "DRUL", "DRLU", "LUDR", "LURD", "ULDR", "ULRD" };
            string workingDirectory = Environment.CurrentDirectory;
            string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\"));

            var files = Directory.GetFiles(projectDir);

            string results = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Results\"));

            foreach (var strategy in strategies)
            {
                foreach (var file in files)
                {
                    if (strategy == "astrM" || strategy == "astrH")
                    {
                        var heuristic = strategy == "astrM" ? "Manhattan" : "Hamming";
                        var splittedResultFileName = file.Substring(file.LastIndexOf(@"\") + 1).Split('.')[0];
                        var baseResultName = $"{results}{splittedResultFileName}_{heuristic}_astr_"; 

                        var solutionFileName = $"{baseResultName}_sol.txt";
                        var resultFileName = $"{baseResultName}stas.txt";
                        var args = new string[]
                        {
                       "astr",
                        heuristic,
                        file,
                        solutionFileName,
                        resultFileName
                        };
                        launchArgs.Add(args);
                    }
                    else
                    {
                        foreach (var search in searchOrders)
                        {
                            var splittedResultFileName = file.Substring(file.LastIndexOf(@"\") + 1).Split('.')[0];
                            var baseResultName = $"{results}{splittedResultFileName}_{strategy}_{search}_";
                            var solutionFileName = $"{baseResultName}_sol.txt";
                            var resultFileName = $"{baseResultName}stas.txt";

                            var args = new string[]
                            {
                            strategy,
                            search,
                            file,
                            solutionFileName,
                            resultFileName
                            };
                            launchArgs.Add(args);
                        }

                    }

                }
            }

            return launchArgs;
>>>>>>> 75787255bb1480c9273fcd082ed5787e5baf3e91
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
