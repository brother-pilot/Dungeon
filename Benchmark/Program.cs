﻿using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dungeon;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class Benchmark
    {
        //private readonly BfsTask myClass = new BfsTask();

        //[Benchmark] //указываем какие методы мы будем запускать на тестирование
        //public void Return_ShortestPaths2()
        //{
        //    var textMap = new[]
        //    {
        //        " C                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "CPC                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           C                                                                            ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       C                                ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",
        //        "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ",

        //        " C                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      "
        //    };
        //    var map = Map.FromLines(textMap);
        //    var paths = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
        //}

        [Benchmark] //указываем какие методы мы будем запускать на тестирование
        public void Return_ShortestPaths3()
        {
            var ttt= new DungeonForm();
        }

        //[Benchmark] //указываем какие методы мы будем запускать на тестирование
        //public void Return_ShortestPaths2()
        //{
        //    var textMap = new[]
        //    {
        //"#P                  #",
        //"#                   #",
        //"#                   #",
        //"#                   #",
        //"#              T    #",
        //"#     T             #",
        //"#            T      #",
        //"#       T           #",
        //"#          C        #",
        //"#                   #",
        //"#        T          #",
        //"#           C       #",
        //"#      T            #",
        //"#             T     #",
        //"#    T              #",
        //"#                   #",
        //"#                   #",
        //"#                   #",
        //"#                  E#"
        //    };
        //    var map = Map.FromLines(textMap);
        //    var paths = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
        //}
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmark>(); //указываем какой класс мы хотим тестировать
        }
    }
}