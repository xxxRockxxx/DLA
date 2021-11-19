using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DLA
{
    public class Output : WorkingWithMatrix
    {
        private int _countLivinCells;
        public void DrawMatrix()
        {
            Thread.Sleep(2000);
            Console.Clear();
            _countLivinCells = 0;
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (_matrix[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        _countLivinCells++;
                    }
                    if (_matrix[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(_matrix[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("Number of living cells:" + _countLivinCells);
            CheckWork();
        }
        private void CheckWork()
        {
            if (_countLivinCells == _numberLivCells)
            {
                _work = false;
            }
        }
        public bool Work
        {
            get
            {
                return _work;
            }

        }
    }
}
