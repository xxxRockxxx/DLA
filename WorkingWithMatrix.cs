using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DLA
{
    class WorkingWithMatrix
    {
        protected static int[,] _matrix;
        protected static int[,] _newMatrix;
        protected static int _lengthX;
        protected static int _lengthY;
        private int _direction;
        private int _x;
        private int _y;
        private int _oldMatrix_x;
        private int _oldMatrix_y;
        protected static bool _work=true;
        protected static int _numberLivCells;
        protected static int _numberParticle;

        public void GenerationMatrix(int numberCells)
        {
            _numberLivCells = numberCells;
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

        public int CheckNumberParticle
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
            while (_oldMatrix_y != _lengthY)
            {
                if (_matrix[_oldMatrix_y, _oldMatrix_x] == 2)
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
            while (_oldMatrix_y != _lengthY)
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
                case 0:
                    _y--;
                break;
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
