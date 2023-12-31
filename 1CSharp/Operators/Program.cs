﻿using System;

namespace Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 14;
            int b = 6;
            int c = 0;

            // 산술연산자
            // ----------------------

            c = a + b;
            Console.WriteLine(c);
            c = a - b;
            Console.WriteLine(c);
            c = a * b;
            Console.WriteLine(c);
            c = a / b; //정수끼리 나눗셈은 몫만 반황
            Console.WriteLine(c);
            c = a % b; // 나머지 연산
            Console.WriteLine(c);

            // 복합 대입 연산자
            //-------------------------------------------------------

            c += a; // c = c + a;
            c -= a;
            c *= a;
            c /= a;
            c %= a;

            // 증감 연산자
            //--------------------------------------------------------

            // 전위연산
            // 연산 내용 : c = c + 1;
            // 반환 값 : c + 1;
            ++c;
            // 후위연산
            // 연산 내용 : c = c + 1;
            // 반환 값 : c;
            c++;
            --c; // c = c - 1;, c -= 1;
            c--;

            // 관계 연산자
            // 두 피연산자의 관계를 비교해서 결과가 참인지 거짓인지 반환
            //------------------------------------------------------------
            bool result;
            result = a == b; // a 와 b 가 같으면 true, 아니면 false
            result = a != b; // a 와 b 가 다르면 true, 아니면 false
            result = a > b;
            result = a >= b;
            result = a < b;
            result = a <= b;

            // 논리 연산자
            // 논리형의 피연산자들에 대해서만 연산 수행
            //------------------------------------------------------------
            bool A = true;
            bool B = false;

            // or 
            // A 와 B 둘중에 하나라도 true 면 true, 아니면 false
            result = A | B;

            // and
            // A 와 B 둘 다 true 일때만 true, 아니면 false
            result = A & B;

            // xor
            // A 와 B 둘중에 하나만 true 면 true, 아니면 false
            result = A ^ B;

            // not
            // A 가 true 면 false, false 이면 true
            result = !A;
            result = A == false;

            // 조건부 논리연산자
            //---------------------------------------------------------------

            // Conditional or
            // A 가 true 이면 논리연산 수행하지않고 true 반환. 아니면 A | B 수행
            result = A || B;

            // Conditional and
            // A 가 false 이면 논리연산 수행하지않고 false 반환. 아니면 A & B 수행
            result = A && B;


            // 비트 연산자
            // 정수형에 대해서만 연산함
            //----------------------------------------------------------------

            // or
            Console.WriteLine(a | b);
            // a == 14 == 2^3 + 2^2 + 2^1 == ... 00001110
            // b ==  6 == 2^2 + 2^1            == ... 00000110
            // ------------------or--------------------
            // result                                    == ... 00001110 == 14

            // and
            Console.WriteLine(a & b);
            // a == 14 == 2^3 + 2^2 + 2^1 == ... 00001110
            // b ==  6 == 2^2 + 2^1            == ... 00000110
            // -----------------and--------------------
            // result                                    == ... 00000110 == 6

            // and
            Console.WriteLine(a ^ b);
            // a == 14 == 2^3 + 2^2 + 2^1 == ... 00001110
            // b ==  6 == 2^2 + 2^1            == ... 00000110
            // -----------------xor--------------------
            // result                                    == ... 00001000 == 8

            // not
            Console.WriteLine(~a);
            // a == 14 == 2^3 + 2^2 + 2^1 == 00000000 00000000 00000000 00001110
            // -----------------not--------------------
            // result                                    == 11111111 11111111 11111111 11110001 == -15

            // 2의 보수 : 2진수 모든 자리 반전 후 + 1 == ~a + 1
            // a == 14 == 2^3 + 2^2 + 2^1 == 00000000 00000000 00000000 00001110
            // -a == -14 == 2^3 + 2^2 + 2^1 == 11111111 11111111 11111111 11110010
            // -a * -1  == 14 == 2^3 + 2^2 + 2^1 == 00000000 00000000 00000000 00001110

            // shift -left
            // 비트를 n 자리만큼 왼쪽으로 밀어라
            Console.WriteLine(a << 2);
            // a == 14 == 2^3 + 2^2 + 2^1 == 00000000 00000000 00000000 00001110
            //---------------------------<<2------------------------------------
            // result                                    ==00000000 00000000 00000000 00111000 = 56

            // shift -right
            Console.WriteLine(a >> 2);
            // a == 14 == 2^3 + 2^2 + 2^1 == 00000000 00000000 00000000 00001110
            //--------------------------->>2------------------------------------
            // result                                    ==00000000 00000000 00000000 00000011 = 3


            // Layers
            // 0 : ground
            // 1 : player
            // 2 : enemy
            // 3 : prop

            // ground checking mask : 00000000 00000000 00000000 00000001
            // 어떤 객체가 감지되었을 때 땅인지 보려면 
            // 해당 객체의 bit flag 값 과 mask 를 & 연산
            Console.WriteLine("Hello World!");
        }
        public class Collider
        {
            public int layer;
            public int collisionMask; // 00000000 00000000 00000000 00001001

            public void OnCollisionEnter(Collider other)
            {
                // enemy
                // 00000000 00000000 00000000 00000100
                // 00000000 00000000 00000000 00001001
                //----------------- & --------------------
                // 00000000 00000000 00000000 00000000

                // prop
                // 00000000 00000000 00000000 00001000
                // 00000000 00000000 00000000 00001001
                //----------------- & --------------------
                // 00000000 00000000 00000000 00001000


                if (((1 << other.layer) & collisionMask) > 0)
                {
                    // something to do when collision occured 
                }
            }
        }

        // ref 키워드 
        // 파라미터를 참조타입으로 쓰겠다는 키워드 ->  즉, 여기 인자로 넣어줄 수 있는건 주소를 가지고 있는 변수 같은 메모리 영역을 넣어줘야함.(상수 못넣어줌)
        int AfterPP(ref int variable)
        {
            int origin = variable;
            variable += 1;
            return origin;
        }

        int BeforePP(ref int variable)
        {
            variable += 1;
            return variable;
        }
    }
}
