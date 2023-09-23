namespace Structure
{
    internal class Program
    {

        // struct 구조체
        // 데이터와 기능을 정의하는 사용자 정의 자료형
        // C#에서 구조체는 클래스를 값 타입으로 사용하기위한 자료형

        // 구조체 vs 클래스
        // 멤버변수(데이터)를 쓰기/읽기가 빈번하게 일어나는 경우 : 값타입이 참조타입보다 유리
        // 항상 유리하지는 않다. --> 16byte 이하일때.
        // 확장의 가능성이 없을 때.
        public struct Vector3
        {
            // property
            public float x
            {
                get {
                    return _x;
                }
                private set { 
                    _x = value; 
                }
            }

            public float y { get => _y; set => _y = value; }
            public float z { get => _z; set => _z = value; }

            float _x;
            float _y;
            float _z;

            // 구조체 생성자
            // 1. 멤버변수의 초기화 내용 구현
            public Vector3()
            {
                this._x = 0;
                this._y = 0;
                this._z = 0;
            }

            public float GetX()
            {
                return this._x;
            }

            public void SetX(float x)
            {
                this._x = x;
            }

            public float Maginitude()
            {
                return (float)Math.Sqrt(_x * _x + _y * _y + _z * _z);
            }
        }

        public class Vector2
        {
            public float x;
            public float y;

            // 클래스 생성자
            // 1. Managed Heap  영역에 객체를 만듬.
            // 2. 멤버 변수 데이터를 데이터영역에서 해당객체를 초기화
            // 3. 해당 객체 주소를 반환
            public Vector2()
            {
                this.x = 0;
                this.y = 0;
            }

            public float Maginitude()
            {
                return (float)Math.Sqrt(x * x + y * y);
            }
        }
        static void Main(string[] args)
        {
            Vector2 vector2 = new Vector2();
            Vector3 vector3 = new Vector3();
            vector3.y = 1.0f;
            Console.WriteLine(vector3.y);
            
        }
    }
}