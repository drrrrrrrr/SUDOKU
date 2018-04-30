using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngSignalRSudoku.Models
{
    public static class Extent
    {
        public static int[] GetLine(this byte[,] ar ,int i)
        {
            int[] br = new int[9];
            for (int j = 0; j < 9; j++)
            {
                br[j] = ar[i, j];
            }
            return br;
        }
        public static byte[,] ToCell(this string[] ar)
        {
            byte[,] br = new byte[9, 9];
            for (int i = 0; i < 9; i++)
            {
                string[] m = ar[i].Split(' ');
                for (int j = 0; j < 9; j++)
                {
                    br[i, j] = byte.Parse(m[j]);
                }
            }
            return br;
        }
    }
    public class SudokuEngine
    {
        
        static private byte[,] GetData()
        {
            byte[,] d;
            string[] sudoku = new string[9];
            sudoku[0] = "0 9 0 0 5 0 2 0 0";
            sudoku[1] = "7 2 0 0 0 1 0 0 0";
            sudoku[2] = "0 0 5 0 8 7 0 0 4";
            sudoku[3] = "1 0 9 0 0 0 0 0 0";
            sudoku[4] = "6 0 0 0 7 0 0 0 1";
            sudoku[5] = "0 0 0 0 0 0 0 0 5";
            sudoku[6] = "0 0 0 0 0 0 0 0 9";
            sudoku[7] = "0 0 0 1 6 4 5 8 0";
            sudoku[8] = "8 0 4 0 9 0 0 6 0";

            d = sudoku.ToCell();


            return d;
        }

        internal static Sudoku Solve(string groupName)
        {
            GenerateSudoku gen = new GenerateSudoku();
            byte[,] d = GetData();
            gen.Data = d;
            ////gen.Generate(20);
            ////d = gen.Data;
            gen.Solve();
            d = gen.Data;
           
            var grid = new GridRow[]
            {
                new GridRow(0, d.GetLine(0)),
                new GridRow(1, d.GetLine(1)),
                new GridRow(2, d.GetLine(2)),
                new GridRow(3, d.GetLine(3)),
                new GridRow(4, d.GetLine(4)),
                new GridRow(5, d.GetLine(5)),
                new GridRow(6, d.GetLine(6)),
                new GridRow(7, d.GetLine(7)),
                new GridRow(8,d.GetLine(8)),
            };

            var sudoku = new Sudoku
            {
                Grid = grid,
                Level = 3,
                GroupName = groupName
            };
            return sudoku;
        }

        public static Sudoku CreateSudoku(string groupName)
        {
            GenerateSudoku gen = new GenerateSudoku();
            byte[,] d = GetData();
            gen.Data = d;
            ////gen.Generate(20);
            ////d = gen.Data;
         
           
            d = gen.Data;

            var grid = new GridRow[] 
            {
                new GridRow(0, d.GetLine(0)),
                new GridRow(1, d.GetLine(1)),
                new GridRow(2, d.GetLine(2)),
                new GridRow(3, d.GetLine(3)),
                new GridRow(4, d.GetLine(4)),
                new GridRow(5, d.GetLine(5)),
                new GridRow(6, d.GetLine(6)),
                new GridRow(7, d.GetLine(7)),
                new GridRow(8,d.GetLine(8)),
            };

            var sudoku = new Sudoku
            {
                Grid = grid,
                Level = 3,
                GroupName = groupName
            };
            return sudoku;
        }
    }
}
