using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tyuiu.UleevRI.Sprint7.Project.V9.Lib
{
    public class DataService
    {
        public string[,] GetMatrix(string[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (c == 8 && matrix[r, c] != "11")
                    {
                        matrix[r, c] = "11";
                    }
                }
            }
            return matrix;
            //
        }
    }
}
