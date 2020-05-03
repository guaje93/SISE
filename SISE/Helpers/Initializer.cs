using SISE.Model;
using SISE.Solution;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SISE
{
    public class Initializer
    {
        #region Fields

        private readonly int[,] _solved = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0 } };
        private readonly State _solvedState = null;

        #endregion

        #region Properties

        public Tuple<int, int> MatrixSize { get; private set; }
        public State InitialState { get; private set; }
        public string SolutionFileDestination { get; private set; }
        public string SolutionInformationDestination { get; private set; }
        public ISolver Solver { get; private set; }

        #endregion

        #region Constructors


        public Initializer(string[] args)
        {
            SolutionFileDestination = args[3];
            SolutionInformationDestination = args[4];
            InitialState = ReadInitialState(args);
            _solvedState = new State(_solved, new Point(3, 3));
            Solver = GetSolver(args);
        }

        #endregion

        #region Methods

        internal static bool Validate(string[] args)
        {
            if (args.Count() != 5)
            {
                Console.WriteLine("Initializer: Wrong number of arguments, given arguments:");
                int nr = 0;
                foreach (var arg in args)
                {
                    Console.WriteLine(nr++ + ": " + arg);
                }
                return false;
            }
            if (args[0] != "bfs" && args[0] != "dfs" && args[0] != "astr")
            {
                Console.WriteLine("Initializer: Wrong algorithm type");
                return false;
            }
            if (args[0] == "bfs" || args[0] == "dfs")
            {
                if (!(args[1].Length == 4 &&
                    args[1].Distinct().Count() == args[1].Length &&
                    args[1].All(p => "DRUL".ToArray().Contains(p))))
                {
                    Console.WriteLine($"Initializer: Wrong strategy: {args[1]}");
                    return false;
                }
            }
            if (args[0] == "astr" && (args[1] != "manh" && (args[1] != "hamm")))
            {
                Console.WriteLine($"Initializer: Wrong strategy: {args[1]}");
                return false;
            }

            Console.WriteLine($"Initializer: Argument validation passed");
            return true;

        }
        private ISolver GetSolver(string[] args)
        {
            switch (args[0])
            {
                case "bfs":
                    return new BreadthFirstSearchSolver(InitialState, args[1], _solvedState);
                case "dfs":
                    return new DepthFirstSearchSolver(InitialState, args[1], _solvedState);
                case "astr":
                    {
                        if (args[1] == "manh")
                            return new AStarSolver(InitialState, new Manhattan(), _solvedState);
                        else if (args[1] == "hamm")
                            return new AStarSolver(InitialState, new Hamming(), _solvedState);
                        else
                            return null;
                    }
                default:
                    Console.WriteLine("Initializer: Wrong type parameter");
                    return null;
            }
        }

        private State ReadInitialState(string[] args)
        {
            string data;
            int[,] puzzle = null;
            Point point = new Point();
            try
            {
                using (StreamReader sr = new StreamReader(args[2]))
                {
                    data = sr.ReadLine();
                    if (data == null || data.Count() != 3)
                    {
                        throw new Exception("Initializer: Dimentions were not specified correctly");
                    }
                    string[] dimentions = data.Split(' ');
                    MatrixSize = Tuple.Create(Convert.ToInt32(dimentions[0]), Convert.ToInt32(dimentions[1]));

                    puzzle = new int[MatrixSize.Item1, MatrixSize.Item2];

                    for (int i = 0; i < MatrixSize.Item1; i++)
                    {
                        string[] values = sr.ReadLine().Split(' ');
                        if (values.Count() != MatrixSize.Item2)
                        {
                            throw new Exception(String.Format("Initializer: Values in row {0} were not specified correctly", i));
                        }
                        for (int j = 0; j < MatrixSize.Item2; j++)
                        {
                            int value = Convert.ToInt32(values[j]);
                            if (value == 0)
                            {
                                point = new Point(i, j);
                            }
                            puzzle[i, j] = value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Initializer: ERR: File could not be read.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(-1);
            }
            return new State(puzzle, point, MatrixSize);
        }

        #endregion
    }
}
