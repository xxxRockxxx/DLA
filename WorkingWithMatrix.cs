using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DLA
{
    class WorkingWithMatrix
    {        
        //Зачем этих четверых делать статическими? У нас же конкретная матрица
        private int[,] _matrix;
        private int[,] _newMatrix;
        private int _lengthX;
        private int _lengthY;
        private int _direction;
        private int _x;
        private int _y;
        private int _oldMatrix_x;
        private int _oldMatrix_y;
        //Аналогично - зачем так баловаться статическими переменными?
        private bool _work=true;
        public int NumberLivCells { get; private set; }//Так аккуратнее
        protected int _numberParticle;

        public int [,] GetMatrix ()
        {
            return _matrix;
        }

        public bool Work//В Output это не нужно)
        {
            get
            {
                return _work;
            }

        }

        public void ShutDown()//А если хочется выключать извне, то лучше так =)
        {
            _work = false;
        }
        public void GenerationMatrix(int numberCells)
        {
            NumberLivCells = numberCells;
            _lengthX = numberCells * 2;
            _lengthY = numberCells * 2;
            _matrix = new int[_lengthX, _lengthY];
            _newMatrix = new int[_lengthX, _lengthY];
            _matrix[_lengthY / 2, _lengthX /2] = 1;
        }

        public void SpawnParticle()
        {
            Random rnd = new Random();
            int dice_x = 0;
            int dice_y = 0;
            
            while (_matrix[dice_y, dice_x] != 2)
            {
                dice_x = rnd.Next(0, _lengthX);
                dice_y = rnd.Next(0, _lengthY);
                if (_matrix[dice_y, dice_x] == 0)
                {
                    _matrix[dice_y, dice_x] = 2;
                }
            }
            _numberParticle++;
        }

        public int CheckNumberParticle //Если что-то называется через Check, оно обычно должно true-false возвращать - проверка же. Но не критично =)
        {
            get
            {
                return _numberParticle;
            }

        }

        public void ParticleMovement()
        {
            _oldMatrix_y = 0;
            _oldMatrix_x = 0;
            int countMove = 0;
            while (_oldMatrix_y != _lengthY)//А почему нельзя это двойным фором перебрать? О_О
            {
                if (_matrix[_oldMatrix_y, _oldMatrix_x] == 2 & countMove!=1)
                {
                    _y = _oldMatrix_y;
                    _x = _oldMatrix_x;
                    while (_newMatrix[_y, _x] != 2)
                    {
                        _y = _oldMatrix_y;
                        _x = _oldMatrix_x;
                        GenerateNumber();
                        ChooseMotion();
                        if ((_y<=_lengthY-1 & _x<=_lengthX-1) & (_y>=0 & _x>=0))
                        {
                            if (_matrix[_y, _x] == 0)
                            {
                                _matrix[_oldMatrix_y, _oldMatrix_x] = 0;
                                _matrix[_y, _x] = 2;
                                _newMatrix[_y, _x] = 2;
                                countMove++;
                            }
                        }
                        else
                        {
                            _y = _oldMatrix_y;
                            _x = _oldMatrix_x;
                        }
                    }
                }
                _newMatrix[_y, _x] = 0;
                _oldMatrix_x++;
                if (_oldMatrix_x == _lengthX)
                {
                    _oldMatrix_x = 0;
                    _oldMatrix_y++;
                }
            }
        }

        public void CheckNeighbors()
        {
            _oldMatrix_y = 0;
            _oldMatrix_x = 0;
            while (_oldMatrix_y != _lengthY)//Ну, тут, мне кажется, можно как-то попроще все-таки
                                            //Проверка соседей - это же просто проверка 8 клеток около заданной с координатами
            /*
             * x-1;y-1
             * x;y-1
             * x+1;y-1
             * x-1;y
             * x+1;y
             * x-1;y+1
             * x;y+1
             * x+1;y+1
            */
            {
                if (_matrix[_oldMatrix_y, _oldMatrix_x] == 2)
                {
                    if (_oldMatrix_y == 0 & _oldMatrix_x == 0)
                    {
                        if (_matrix[_oldMatrix_y + 1, _oldMatrix_x] == 1 || _matrix[_oldMatrix_y + 1, _oldMatrix_x + 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x + 1] == 1)
                        {
                            _matrix[_oldMatrix_y, _oldMatrix_x] = 1;
                            _numberParticle--;
                        }
                    }

                    else if (_oldMatrix_x == 0 & (_oldMatrix_y != 0 & _oldMatrix_y != _lengthY - 1))
                    {
                        if (_matrix[_oldMatrix_y + 1, _oldMatrix_x] == 1 || _matrix[_oldMatrix_y + 1, _oldMatrix_x + 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x + 1] == 1 ||
                            _matrix[_oldMatrix_y - 1, _oldMatrix_x + 1] == 1 || _matrix[_oldMatrix_y - 1, _oldMatrix_x] == 1)
                        {
                            _matrix[_oldMatrix_y, _oldMatrix_x] = 1;
                            _numberParticle--;
                        }
                    }

                    else if (_oldMatrix_x == 0 & _oldMatrix_y == _lengthY - 1)
                    {
                        if (_matrix[_oldMatrix_y - 1, _oldMatrix_x] == 1 || _matrix[_oldMatrix_y - 1, _oldMatrix_x + 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x + 1] == 1)
                        {
                            _matrix[_oldMatrix_y, _oldMatrix_x] = 1;
                            _numberParticle--;
                        }
                    }

                    else if (_oldMatrix_y == 0 & (_oldMatrix_x != 0 & _oldMatrix_x != _lengthX - 1))
                    {
                        if (_matrix[_oldMatrix_y + 1, _oldMatrix_x] == 1 || _matrix[_oldMatrix_y + 1, _oldMatrix_x + 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x + 1] == 1 ||
                            _matrix[_oldMatrix_y + 1, _oldMatrix_x - 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x - 1] == 1)
                        {
                            _matrix[_oldMatrix_y, _oldMatrix_x] = 1;
                            _numberParticle--;
                        }
                    }

                    else if (_oldMatrix_x == _lengthX - 1 & _oldMatrix_y == 0)
                    {
                        if (_matrix[_oldMatrix_y +1, _oldMatrix_x] == 1 || _matrix[_oldMatrix_y + 1, _oldMatrix_x - 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x - 1] == 1)
                        {
                            _matrix[_oldMatrix_y, _oldMatrix_x] = 1;
                            _numberParticle--;
                        }
                    }

                    else if (_oldMatrix_x==_lengthX-1 &(_oldMatrix_y != 0 & _oldMatrix_y != _lengthY - 1))
                    {
                        if (_matrix[_oldMatrix_y + 1, _oldMatrix_x] == 1 || _matrix[_oldMatrix_y + 1, _oldMatrix_x - 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x - 1] == 1 ||
                            _matrix[_oldMatrix_y - 1, _oldMatrix_x - 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x - 1] == 1)
                        {
                            _matrix[_oldMatrix_y, _oldMatrix_x] = 1;
                            _numberParticle--;
                        }
                    }
                    
                    else if (_oldMatrix_x==_lengthX-1 & _oldMatrix_y== _lengthY - 1)
                    {
                        if (_matrix[_oldMatrix_y - 1, _oldMatrix_x] == 1 || _matrix[_oldMatrix_y - 1, _oldMatrix_x - 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x - 1] == 1)
                        {
                            _matrix[_oldMatrix_y, _oldMatrix_x] = 1;
                            _numberParticle--;
                        }
                    }

                    else if (_oldMatrix_y==_lengthY-1 &(_oldMatrix_x!=_lengthX-1 && _oldMatrix_x != 0))
                    {
                        if (_matrix[_oldMatrix_y - 1, _oldMatrix_x] == 1 || _matrix[_oldMatrix_y - 1, _oldMatrix_x - 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x - 1] == 1 ||
                            _matrix[_oldMatrix_y - 1, _oldMatrix_x + 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x + 1] == 1)
                        {
                            _matrix[_oldMatrix_y, _oldMatrix_x] = 1;
                            _numberParticle--;
                        }
                    }

                    else if((_oldMatrix_y != _lengthY - 1 & _oldMatrix_x != _lengthX - 1) & (_oldMatrix_y != 0 & _oldMatrix_x != 0))
                    {
                        if (_matrix[_oldMatrix_y - 1, _oldMatrix_x] == 1 || _matrix[_oldMatrix_y - 1, _oldMatrix_x - 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x - 1] == 1 ||
                            _matrix[_oldMatrix_y - 1, _oldMatrix_x + 1] == 1 || _matrix[_oldMatrix_y, _oldMatrix_x + 1] == 1 || _matrix[_oldMatrix_y+1, _oldMatrix_x - 1] == 1 ||
                            _matrix[_oldMatrix_y + 1, _oldMatrix_x ] == 1 || _matrix[_oldMatrix_y + 1, _oldMatrix_x + 1] == 1)
                        {
                            _matrix[_oldMatrix_y, _oldMatrix_x] = 1;
                            _numberParticle--;
                        }
                    }
                }
                _oldMatrix_x++;
                if (_oldMatrix_x == _lengthX)
                {
                    _oldMatrix_x = 0;
                    _oldMatrix_y++;
                }
            }
        }

        private void GenerateNumber()
        {
            Random rnd = new Random();
            _direction = rnd.Next(0, 8);
        }

        private void ChooseMotion()
        {
            switch(_direction)
            {
                case 0://Синтаксис Switch-case такой
                    {
                        _y--;
                        break;
                    }
                    
                case 1:
                    _y++;
                break;
                case 2:
                    _x--;
                break;
                case 3:
                    _x++;
                break;
                case 4:
                    _x++;
                    _y++;
                break;
                case 5:
                    _y++;
                    _x--;
                break;
                case 6:
                    _x--;
                    _y--;
                break;
                case 7:
                    _x++;
                    _y--;
                break;
            }
        }
    }
}
