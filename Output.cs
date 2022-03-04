using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DLA
{
    class Output//Зачем OutPut наследовать от матрицы? Это разные классы с разным функционалом!
    {
        private int _countLivinCells;
        private WorkingWithMatrix w_matrix;//Достаточно просто вот так сделать =)
        public void DrawMatrix()
        {
            Thread.Sleep(2000);
            Console.Clear();
            _countLivinCells = 0;
            for (int i = 0; i < w_matrix.GetMatrix().GetLength(0); i++) //GetMatrix(0) возвращает массив. То есть, для шарпа можно с ним обращаться как с массивом))
            {
                for (int j = 0; j < w_matrix.GetMatrix().GetLength(1); j++)
                {
                    if (w_matrix.GetMatrix()[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        _countLivinCells++;
                    }
                    if (w_matrix.GetMatrix()[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(w_matrix.GetMatrix()[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("Number of living cells:" + _countLivinCells);
            //Ну, вообще подсчет живых клеток можно делать прямо в workingmatrix, и на основе этого уже решать, выключать или нет программу. Draw должен draw)))
            CheckWork();
        }
        private void CheckWork()
        {
            if (_countLivinCells == w_matrix.NumberLivCells)
            {
                w_matrix.ShutDown();
            }
        }
        
    }
}
