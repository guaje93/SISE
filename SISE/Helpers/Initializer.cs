﻿using SISE.Solution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace SISE
{
    public class Initializer

    {
        private readonly int[,] _solved = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0 } };
        private readonly State _solvedState = null;

        public Tuple<int, int> MatrixSize { get; private set; }
        public State InitialState { get; private set; }
        public string SolutionFileDestination { get; private set; }
        public string SolutionInformationDestination { get; private set; }
        public ISolver Solver { get; private set; }


        public Initializer(string[] args)
        {
            SolutionFileDestination = args[3];
            SolutionInformationDestination = args[4];
            InitialState = ReadInitialState(args);
            _solvedState = new State(_solved, new Point(3, 3));
            Solver = GetSolver(args);

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
                    return new AStarSolver(InitialState, args[1], _solvedState);
                default:
                    Console.WriteLine("wrong type parameter");
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
                        throw new Exception("Dimentions were not specified correctly");
                    }
                    string[] dimentions = data.Split(' ');
                    MatrixSize = Tuple.Create(Convert.ToInt32(dimentions[0]), Convert.ToInt32(dimentions[1]));

                    puzzle = new int[MatrixSize.Item1, MatrixSize.Item2];

                    for (int i = 0; i < MatrixSize.Item1; i++)
                    {
                        string[] values = sr.ReadLine().Split(' ');
                        if (values.Count() != MatrixSize.Item2)
                        {
                            throw new Exception(String.Format("Values in row {0} were not specified correctly", i));
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
                Console.WriteLine("ERR: File could not be read.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(-1);
            }
            return new State(puzzle, point, MatrixSize);

        }

    }
}