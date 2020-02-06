using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp6
{
    struct Point
    {
        public int yPos, xPos;
    }

    enum Algorithm
    {
        LeftHand,
        RightHand
    }

    enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }

    static class Tools
    {
        public static char[,] CloneMaze(this char[,] maze)
        {
            var cloneMaze = new char[maze.GetLength(0), maze.GetLength(1)];
            for (int y = 0; y < maze.GetLength(0); y++)
            {
                for (int x = 0; x < maze.GetLength(1); x++)
                {
                    cloneMaze[y, x] = maze[y, x];
                }
            }
            return cloneMaze;
        }
    }

    class Program
    {
        private static readonly char[,] _maze1 =
        {
            //Y/ X  0     1     2     3     4     5     6     7     8     9
            /*0*/{ 'W',  '\0', 'W',  'W',  'W',  'W',  'W',  'W',  '\0', 'W'  },
            /*1*/{ 'W',  '\0', '\0', 'W',  '\0', '\0', '\0', 'W',  '\0', 'W'  },
            /*2*/{ 'W',  'W',  '\0', 'W',  '\0', 'W',  '\0', 'W',  '\0', 'W'  },
            /*3*/{ 'W',  '\0', '\0', '\0', '\0', 'W',  '\0', 'W',  '\0', 'W'  },
            /*4*/{ 'W',  'W',  'W',  'W',  'W',  '\0', '\0', 'W',  'W',  'W'  },
            /*5*/{ 'W',  '\0', 'W',  '\0', '\0', 'W',  '\0', 'W',  '\0', '\0' },
            /*6*/{ '\0', '\0', '\0', '\0', '\0', 'W',  '\0', '\0', '\0', 'W'  },
            /*7*/{ 'W',  'W',  'W',  'W',  '\0', 'W',  '\0', 'W',  '\0', 'W'  },
            /*8*/{ 'W',  '\0', '\0', '\0', '\0', 'W',  '\0', '\0', '\0', 'W'  },
            /*9*/{ 'W',  'W',  '\0', 'W',  'W',  'W',  'W',  'W',  'W',  'W'  }
        };

        private static readonly char[,] _maze2 =
        {
            //Y/  X  0    1     2     3     4     5     6     7     8     9    10
            /*0*/{  'W', 'W',  'W',  'W',  'W',  'W',  'W',  'W',  'W',  'W',  'W'},
            /*1*/{  'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            /*2*/{  'W', '\0', 'W',  'W',  'W',  '\0', 'W',  'W',  'W',  '\0', 'W'},
            /*3*/{  'W', '\0', 'W',  '\0', 'W',  '\0', 'W',  '\0', 'W',  '\0', 'W'},
            /*4*/{  'W', '\0', 'W',  'W',  'W',  '\0', 'W',  'W',  'W',  '\0', 'W'},
            /*5*/{  'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            /*6*/{  'W', '\0', 'W',  'W',  'W',  '\0', 'W',  'W',  'W',  '\0', 'W'},
            /*7*/{  'W', '\0', 'W',  '\0', 'W',  '\0', 'W',  '\0', 'W',  '\0', 'W'},
            /*8*/{  'W', '\0', 'W',  'W',  'W',  '\0', 'W',  'W',  'W',  '\0', 'W'},
            /*9*/{  'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            /*10*/{ 'W', 'W',  'W',  'W',  'W',  'W',  'W',  'W',  'W',  'W',  'W'}
        };

        private static readonly char[,] _maze =
        {
            {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'},
            {'W', '\0', 'W', '\0', '\0', '\0', 'W', '\0', '\0', '\0', '\0', '\0', '\0', 'W', '\0', '\0', 'W', '\0', '\0', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', 'W', 'W', 'W', 'W', '\0', 'W', '\0', '\0', '\0', 'W', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', '\0', '\0', 'W', '\0', 'W', '\0', '\0', '\0', '\0', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', 'W', 'W', 'W', 'W', '\0', 'W', 'W', '\0', 'W', 'W', '\0', 'W', 'W', '\0', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', '\0', '\0', 'W', '\0', 'W', '\0', '\0', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', '\0', '\0', 'W', '\0', 'W', 'W', '\0', 'W', '\0', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', '\0', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', '\0', '\0', '\0', 'W', 'W', '\0', 'W', '\0', '\0', 'W', '\0', 'W', '\0', '\0', '\0', '\0', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', '\0', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', 'W', 'W', 'W', '\0', 'W', '\0', '\0', 'W', 'W', 'W', 'W', 'W', '\0', 'W', 'W', 'W'},
            {'W', '\0', 'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            {'W', '\0', '\0', '\0', 'W', 'W', 'W', 'W', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', 'W', 'W', '\0', 'W', 'W', '\0', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', '\0', 'W', 'W', 'W'},
            {'W', '\0', '\0', '\0', 'W', '\0', 'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W', '\0', '\0', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W', '\0', 'W'},
            {'W', 'W', 'W', '\0', 'W', 'W', 'W', 'W', '\0', 'W', 'W', '\0', 'W', '\0', 'W', 'W', '\0', 'W', '\0', 'W', 'W'},
            {'W', '\0', '\0', '\0', 'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W', '\0', 'W', '\0', '\0', '\0', '\0', '\0', 'W'},
            {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'}
        };

        private static readonly char[,] _maze4 =
        {
            {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'},
            {'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W', '\0', 'W'},
            {'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W', 'W', 'W', '\0', 'W'},
            {'W', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'W'},
            {'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'}
        };


        static bool CanMove(char[,] maze, Point point, Direction direction)
        {
            if (point.yPos < 0 || point.xPos < 0 || point.yPos >= maze.GetLength(0) || point.xPos >= maze.GetLength(1)) return false;
            return direction switch
            {
                Direction.Left => (point.xPos - 1 >= 0 && maze[point.yPos, point.xPos - 1] == '\0'),
                Direction.Up => (point.yPos - 1 >= 0 && maze[point.yPos - 1, point.xPos] == '\0'),
                Direction.Right => (point.xPos + 1 < maze.GetLength(1) && maze[point.yPos, point.xPos + 1] == '\0'),
                Direction.Down => (point.yPos + 1 < maze.GetLength(0) && maze[point.yPos + 1, point.xPos] == '\0'),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction,
                    "Некорректное направление движения")
            };
        }

        static bool IsExit(char[,] maze, Point point, Direction direction, int step)
        {
            if (point.yPos < 0 || point.xPos < 0 || point.yPos >= maze.GetLength(0) || point.xPos >= maze.GetLength(1)) return false;
            if (maze[point.yPos, point.xPos] != '\0') return false;
            return direction switch
            {
                Direction.Left => point.xPos == 0,
                Direction.Up => point.yPos == 0,
                Direction.Right => point.xPos == maze.GetLength(1) - 1,
                Direction.Down => point.yPos == maze.GetLength(0) - 1,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction,
                    "Некорректное направление движения")
            };
        }

        //НА УДАЛЕНИЕ
        /*class PointEqualityComparer : IEqualityComparer<Point>
        {
            public bool Equals(Point pointA, Point pointB)
                => pointA.yPos == pointB.yPos && pointA.xPos == pointB.xPos;

            public int GetHashCode(Point point) => point.yPos ^ point.xPos;
        }*/




        static Point[] GoThroughMaze(char[,] originalMaze, Point point)
        {
            // Клонирование лабиринта для последующего изменения
            var maze = originalMaze.CloneMaze();

            // Хранение пути
            var way = new List<Point>() {point};

            #region Выбор алгоритма и направления

            var algorithm = (Algorithm) new Random().Next(2);
            var direction = (Direction) new Random().Next(4);

            #endregion

            // Предыдущая точка, для проверки дубликатов
            var previousPoint = point;
            // Точка старта
            var startPoint = point;
            // Точка блокировки для закрытия изученного пути
            var lockPoint = new Point() { yPos = -1, xPos = -1 };
            var lockPointFound = false;

            for (int step = 0; !IsExit(maze, point, direction, step); step++)
            {
                if (algorithm == Algorithm.LeftHand)
                {
                    #region Алгоритм левой руки

                    if (direction == Direction.Left)
                    {
                        if (CanMove(maze, point, Direction.Down))
                        {
                            point.yPos++;
                            direction = Direction.Down;
                        }
                        else if (!CanMove(maze, point, Direction.Left)) direction = Direction.Up;
                        else
                        {
                            point.xPos--;
                        }
                    }
                    else if (direction == Direction.Up)
                    {
                        if (CanMove(maze, point, Direction.Left))
                        {
                            point.xPos--;
                            direction = Direction.Left;
                        }
                        else if (!CanMove(maze, point, Direction.Up)) direction = Direction.Right;
                        else
                        {
                            point.yPos--;
                        }
                    }
                    else if (direction == Direction.Right)
                    {
                        if (CanMove(maze, point, Direction.Up))
                        {
                            point.yPos--;
                            direction = Direction.Up;
                        }
                        else if (!CanMove(maze, point, Direction.Right)) direction = Direction.Down;
                        else
                        {
                            point.xPos++;
                        }
                    }
                    else if (direction == Direction.Down)
                    {
                        if (CanMove(maze, point, Direction.Right))
                        {
                            point.xPos++;
                            direction = Direction.Right;
                        }
                        else if (!CanMove(maze, point, Direction.Down)) direction = Direction.Left;
                        else
                        {
                            point.yPos++;
                        }

                    }

                    #endregion
                }
                else if (algorithm == Algorithm.RightHand)
                {
                    #region Алгоритм правой руки

                    if (direction == Direction.Left)
                    {
                        if (CanMove(maze, point, Direction.Up))
                        {
                            point.yPos--;
                            direction = Direction.Up;
                        }
                        else if (!CanMove(maze, point, Direction.Left)) direction = Direction.Down;
                        else
                        {
                            point.xPos--;
                        }
                    }
                    else if (direction == Direction.Up)
                    {
                        if (CanMove(maze, point, Direction.Right))
                        {
                            point.xPos++;
                            direction = Direction.Right;
                        }
                        else if (!CanMove(maze, point, Direction.Up)) direction = Direction.Left;
                        else
                        {
                            point.yPos--;
                        }
                    }
                    else if (direction == Direction.Right)
                    {
                        if (CanMove(maze, point, Direction.Down))
                        {
                            point.yPos++;
                            direction = Direction.Down;
                        }
                        else if (!CanMove(maze, point, Direction.Right)) direction = Direction.Up;
                        else
                        {
                            point.xPos++;
                        }
                    }
                    else if (direction == Direction.Down)
                    {
                        if (CanMove(maze, point, Direction.Left))
                        {
                            point.xPos--;
                            direction = Direction.Left;
                        }
                        else if (!CanMove(maze, point, Direction.Down)) direction = Direction.Right;
                        else
                        {
                            point.yPos++;
                        }
                    }

                    #endregion
                }

                // Проверка дубликатов
                if (previousPoint.yPos == point.yPos && previousPoint.xPos == point.xPos) continue;
                previousPoint = point;

                // Устанавливаем точку блокировки для стены
                if (!lockPointFound && (startPoint.yPos != point.yPos || startPoint.xPos != point.xPos))
                {
                    lockPoint = point;
                    lockPointFound = true;
                }

                // Устанавливаем стену
                if (startPoint.yPos == point.yPos && startPoint.xPos == point.xPos)
                {
                    maze[lockPoint.yPos, lockPoint.xPos] = 'W';
                    lockPointFound = false;
                }


                // Запись пути
                way.Add(point);


                //Болванчик, НА УДАЛЕНИЕ
                Console.CursorVisible = false;
                for (int y = 0; y < maze.GetLength(0); y++)
                {
                    for (int x = 0; x < maze.GetLength(1); x++)
                    {
                        Console.SetCursorPosition(x * 2, y);
                        Console.Write(maze[y, x]);
                    }
                }
                Console.SetCursorPosition(point.xPos * 2, point.yPos);
                Console.Write('*');
                Thread.Sleep(TimeSpan.FromSeconds(0.05));
                Console.Write("\b\0");

            }

            return way.ToArray();
        }

        static void Main(string[] args)
        {
            /*Console.CursorVisible = false;
            for (int y = 0; y < _maze.GetLength(0); y++)
            {
                for (int x = 0; x < _maze.GetLength(1); x++)
                {
                    Console.SetCursorPosition(x * 2, y);
                    Console.Write(_maze[y, x]);
                }
            }*/
            Point[] points = GoThroughMaze(_maze, new Point() {yPos = 1, xPos = 1});
            /*foreach (var point in points)
            {
                Console.SetCursorPosition(point.xPos * 2, point.yPos);
                Console.Write('*');
                Thread.Sleep(TimeSpan.FromSeconds(0.05));
                Console.Write("\b\0");
            }*/

            Console.Read();
        }
    }
}
