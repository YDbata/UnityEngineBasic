using System;

namespace Array
{
    class Program
    {
        // 2차원 배열
        // 아이템이 6개 각 아이템은 5개의 서브아이템
        // 6개의 행, 5개의 열
        // 0 : 길
        // 1 : 벽
        // 2 : 도착지점
        // 3 : 플레이어
        static int[,] map = new int[6, 5]{
                {0,0,0,0,1 },
                {0,1,1,1,1 },
                {0,0,0,1,1 },
                {1,1,0,1,1 },
                {1,1,0,1,1 },
                {1,1,0,0,2 }
        };
        static int x, y;
        static void Main(string[] args)
        {
            #region 1 차원 배열 및 반복문
            /*
            
            int[] arr = new int[5];

            arr[0] = 1;
            arr[1] = 2;
            // [] 인덱서 :  인덱스 접근하기위한 연산자
            // 0을 인덱스 : 인덱서에 몇번째 인덱스에 접근할건지에 대한 입력
            // 배열의 인덱스 접근 : "배열참조 주소 + 인덱스*자료형 크기" 부터 자료형 크기 만큼 접근

            // 배열을 만들때 초기값을 명시하지 않으면 전부 default값으로 초기화
            // 배열도 멤버 변수, 함수 등을 가지는 클래스 형태
            Console.WriteLine(arr[0]); // 배열의 인덱스는 0~ Array.Length - 1 까지

            int index = 0;
            while (index < arr.Length)
                Console.WriteLine(arr[index++]);
            index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[index]);
            }

            int[] arr2 = new int[10];
            System.Array.Copy(arr, arr2, arr.Length);

            Console.WriteLine("Hello World!");
            */
            #endregion
            int goalY = 5;
            int goalX = 4;

            map[y, x] = 3;
            DrawMap();
            ConsoleKeyInfo kinput;
            while (y != goalY || x != goalX)
            {
                //string input = Console.ReadLine();
                kinput = Console.ReadKey();
                //input = input.ToUpper();
                if (kinput.Key == ConsoleKey.U || kinput.Key == ConsoleKey.UpArrow) MoveUp();
                else if (kinput.Key == ConsoleKey.D || kinput.Key == ConsoleKey.DownArrow) MoveDown();
                else if (kinput.Key == ConsoleKey.L || kinput.Key == ConsoleKey.LeftArrow) MoveLeft();
                else if (kinput.Key == ConsoleKey.R || kinput.Key == ConsoleKey.RightArrow) MoveRight();
                else Console.WriteLine("잘못된 입력.");
            }
            Console.WriteLine("축하합니다!");
            
        }

        static void DrawMap()
        {
            Console.WriteLine("-----");
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int  x = 0;  x < map.GetLength(1);  x++)
                {
                    if (map[y, x] == 0)
                        Console.Write("□");
                    else if (map[y, x] == 1)
                        Console.Write("■");
                    else if (map[y, x] == 2)
                        Console.Write("★");
                    else if(map[y, x] == 3)
                        Console.Write("▣");

                }
                Console.WriteLine();
            }
            Console.WriteLine("-----");
        }

        static void MoveRight()
        {
            if(x >= map.GetLength(1) - 1 || map[y, x+1] == 1)
            {
                Console.WriteLine("오른쪽 방향으로 움직일 수 없습니다.");
            }
            else
            {
                map[y, x] = 0;
                x++;
                map[y, x] = 3;
                DrawMap();
            }
            
        }

        static void MoveUp() {
            if (y < 1 || map[y - 1, x] == 1)
            {
                Console.WriteLine("위 방향으로 움직일 수 없습니다.");
            }
            else
            {
                map[y, x] = 0;
                y--;
                map[y, x] = 3;
                DrawMap();
            }
        }
        static void MoveLeft() {
            if (x < 1 || map[y, x - 1] == 1)
            {
                Console.WriteLine("왼쪽 방향으로 움직일 수 없습니다.");
            }
            else
            {
                map[y, x] = 0;
                x--;
                map[y, x] = 3;
                DrawMap();
            }
        }
        static void MoveDown() {
            if (y >= map.GetLength(0) - 1 || map[y + 1, x] == 1)
            {
                Console.WriteLine("아래 방향으로 움직일 수 없습니다.");
            }
            else
            {
                map[y, x] = 0;
                y++;
                map[y, x] = 3;
                DrawMap();
            }
        }

        // 분기문
        // break; 현재 구문을 탈출
        // continue; 현재 구문흐름을 종료하고 재시작
        // return; 현재 할당된 함수를 종료하고 메모리 해제
    }
}
