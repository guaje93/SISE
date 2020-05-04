using SISE.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SISE
{
    class Program
    {
        #region Fields

        #endregion

        #region Methods


        static void Main(string[] args)
        {
            int maxConcurrency = 10;

            List<string[]> argumentList = LoadArguments();



            using (SemaphoreSlim concurrencySemaphore = new SemaphoreSlim(maxConcurrency))
            {
                var tasks = new List<Task>();

                foreach (var item in argumentList)
                {
                    tasks.Add(ProcessPuzzleAsync(item, concurrencySemaphore));
                }

                var result = Task.WhenAll(tasks);
                result.Wait();
            }
        }

        private static async Task ProcessPuzzleAsync(string[] argument, SemaphoreSlim concurrencySemaphore)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    Initializer initializer = Initialize(argument);
                    RunSolver(initializer);
                }
                finally
                {
                    concurrencySemaphore.Release();
                }
            });
        }

        private static List<string[]> LoadArguments()
        {
            List<string[]> launchArgs = new List<string[]>();
            string[] strategies = { "astrM", "astrH" };
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
                        var heuristic = strategy == "astrM" ? "manh" : "hamm";
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
        }

        private static Initializer Initialize(string[] args)
        {
            if (!Initializer.Validate(args))
            {
                Environment.Exit(-1);
            }
            var initializer = new Initializer(args);
            return initializer;
        }

        private static void RunSolver(Initializer initializer)
        {
            //Console.WriteLine($"Solver {initializer.Solver.GetType().Name}: start processing at {System.DateTime.Now.ToShortTimeString()}");
            var stopwatch = Stopwatch.StartNew();
            var solutionString = initializer.Solver.Solve();
            stopwatch.Stop();
            //Console.WriteLine($"Solver {initializer.Solver.GetType().Name}: finished in {stopwatch.ElapsedMilliseconds}ms");
            SaveResult(initializer, solutionString, stopwatch);

        }

        private static void SaveResult(Initializer initializer, string solutionString, Stopwatch stopwatch)
        {
            var resultGenerator = new ResultGenerators();
            int solutionLength = solutionString != "No solution found!" ? solutionString.Length : -1;
            var resultSaved = resultGenerator.WriteSolution(initializer.SolutionFileDestination, solutionString);
            var additionalResultSaved = resultGenerator.WriteAdditionalInformation(initializer.SolutionInformationDestination,
                                                       solutionLength,
                                                       initializer.Solver.StatesVisitedAmount,
                                                       initializer.Solver.StatesProcessedAmount,
                                                       initializer.Solver.MaxDepth,
                                                       stopwatch.ElapsedMilliseconds);
            if (resultSaved && additionalResultSaved)
            {
                //Console.WriteLine("Result saved");
                //Console.WriteLine($"Additional information saved");
            }
        }

        #endregion
    }
}
