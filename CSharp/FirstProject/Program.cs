// C# 기본 설명

// 색에 따른 구분
// 파란색 : 키워드(예약어) -> C#에서 정해져 있는 단어
// 흰색   : 식별문자(고유이름)
// 청록색 : 클래스 타입이름
// 노란색 : 함수 이름
// 하늘색 : 파라미터(매개변수)
// 주황색 : 문자
// 회색 및 어두운색 : 컴파일러가 불필요하다고 판단되는 구문


namespace FirstProject
{
    // 철수 객체에 필요한 메모리 : 8byte(멤버변수 크기 총 합)
    class cheolsu
    {
        int money;
        uint 나이;

        public cheolsu(int money, uint 나이)
        {
            this.money = money;
            this.나이 = 나이;
        }
    }

    // 영희 객체에 필요한 메모리 : 8byte(멤버변수 크기 총 합)
    class yeonghui
    {
        // 전역변수: 어디서나 접근가능한 변수
        // 클래스 멤버변수도 전역변수임
        public int 나이 = 1; //변수 선언부터 어떤 값을 대입하겠다는 대입연산자를 쓰면 


        // 해당 값을 초기 데이터값으로 쓰겠다는 명시(Data영역)
        float 키;

        // 정적 : 동적의 반대 -> 동적으로 할당할수 없다. 런타임중에 메모리에 추가적인 할당이 불가능
        // static 을 쓰면 클래스에서 메모리를 따로 할당하지 않아도 되므로 클래스의 size가 늘지 않는다.
        static char 성별 = '여';


        // 접근제한자(Access Modigier) : 외부에서 멤버접근 가능여부를 제한하는 키워드
        // private(default값): 이 클래스 외에는 접근을 허용하지 않음
        // protected : 상속받은 자식클래스만 접근 가능하다.
        // internal : Assembly(코드조각 exe, dll 등) 단위로만 해당멤버에 접근 가능
        // public : 접근제한 없음

        // 클래스는 캡슐화를 컨셉으로한 사용자정의자료형임
        // -> 접근제한자를 명시하지않으면 외부접근을 차단하는게 기본컨셉(default가 private)

        // 클래스 생성자
        // 1) 힙메모리 영역에 객체를 할당
        // 2) 사용자가 지정한 멤버변수를 초기화
        // 3) 생서된 객체의 주소를 반환
        public yeonghui()
        {
            this.나이 = 1;
        }

        // 해당객체가 할당된 메모리 영역을 시스템에 반환하는 함수
        // Garbage Collector가 알아서 소멸시키기 때문에 직접 호출할 일은 없다.
        ~yeonghui()
        {

        }

        // void : 타입이 정해지지 않음(반환되지 않음)
        public void SayMyAge(char name)
        {
            // todo -> 이름 출력하기
            Console.WriteLine("{0}의 나이는 {1}이다.", this.GetType(), 나이);

        }

        // 지역변수 : {}내에서 정의되어 해당연산 중에만 메모리가 할당되는 변수
        // 파라미터도 지역변수의 일종
        // 인스턴스 멤버 함수는 데이터를 참조할 목표 객체에 대한 참조 파라미터가 생략
        // -> this로 해당객체 참조가능
        // -> this 키워드도 함수내에서 생략 가능
        public bool UpAge(int uage)
        {
            int 예상나이 = 나이 + uage;
            // 만약 예상나이가 음수면 나이를 먹을 수 없음
            if(예상나이 < 0)
            {
                Console.WriteLine("나이를 먹을 수 없음.");
                return false;
            }

            나이 = 예상나이;

            return true;
           
        }

    }

    internal class Program
    {
        // 변수 : 아직 정해지지 않은 값.

        // 멤버 변수 : 사용자 정의 자료형에서 특정 공간을 구성하는 구성원으로 정의된 변수
        // 아래 적은 변수들은 Program class의 멤버라고 한다.
        int pInt;
        uint puInt;
        short pShort;
        ushort pInt16;
        long pLong;
        ulong pUlong;

        char pChar;

        float pFloat;
        double pDouble;

        bool pBool;

        // 메인함수, 프로그램 실행시 처음 호출되는(실행되는) 함수
        static void Main(string[] args)
        {
            // 문자열형 string은 클래스 타입이기 때문에, 
            // 힙영역에 객체를 만들고 해당 참조를 사용함
            // 힙영역에 할당하는 객체의 크기는 문자갯수*2byte + 1byte(null byte)
            // null byte가 붙는 이유는 문자열의 끝을 명시하기 위함. : \0
            string string1 = "Something blabla"; // ""안에 있으면 문자열 상수임

            // .은 멤버 접근 연산자
            Console.WriteLine("Hello, World!");

            // 값 타입 vs 참조타입
            // 값 타입 : 데이터를 직접 메모리에 쓰고 읽는 타입
            // 참조 타입 : 특정 메모리 주소를 통해 간접적으로 해당 메모리에 데이터를 쓰고 읽는 타입
            // 포인터 타입 : 메모리 주소를 쓰고 읽는 타입(C#에서는 잘 쓰지 않음: unsafe 코드 작성에 쓰기 때문)

            int a = 1;

            // new : 동적할당 키워드(메모리를 동적으로 할당하겠다고 명시하는 키워드)
            // 주소참조 변수
            yeonghui 영희1 = new yeonghui(); // 영희 객체 만들고 영희가 있는 주소를 영희1에 저장
            yeonghui 영희2 = new yeonghui();
            
            Console.WriteLine(영희1.UpAge(1));
            Console.WriteLine(영희1.나이);
            Console.WriteLine(영희2.나이);

            string somethingToPrint = "영희의 나이는 " + 영희1.나이 + "살 입니다.";
            Console.WriteLine(somethingToPrint);
            Console.WriteLine("영희의 나이는 {0}살입니다.", 영희1.나이);
            // 영희1.나이 뒤에 ToString생략


            // 암시적 캐스팅이 가능(small size -> big size)
            int int1 = 1;
            long long1 = int1; // 승격(promotion) : 크기가 더 큰 자료형 레지스터로 작은 데이터를 읽었을 때
            // 읽는 과정에서 해당 데이터의 자료형이 자동으로 바뀌는 효과

            // 형변환(명시적 캐스팅) 해야 가능(big size -> small size)
            long long2 = 2;
            int int2 = (int)long2;

            cheolsu 철수 = new cheolsu(0, 20);
            //영희.SayMyAge(nameof(영희));

        }
    }

    
}