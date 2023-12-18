using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using test.AdventOfCode.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace test.AdventOfCode
{
    internal class AdventOfCode3
    {
        private readonly char[,] _grid = ParseData(Day3.Data);
        private char this[int x, int y]
        {
            get { return _grid[x, y]; }           
        }
        private static char[,] ParseData(string s)
        {
            string[] data = s.Split("\r\n");
            char[,] ParsedData = new char[data.Length, data[0].Length];
            for(int i = 0; i < data.Length; i++)
            {
                //rows
                for (int j = 0; j < data[0].Length; j++)
                {
                    //coloms
                    ParsedData[i, j] = data[i][j];
                }
            }
            return ParsedData;
        }
    }
}
