using System.Security.Cryptography.X509Certificates;

namespace EnumTypes
{
    internal class Program
    {
        // enum 사용자 정의 자료형(열거형)
        // 상수값에 대한 이름 목록을 작성할 수 있다.
        // State type으로 enum을 선언
        // 기본적으로 uint 데이터와 같이 생김
        public enum State
        {
            None,
            Idle,
            Move,
            Jump,
            Fall,
            Attack = 20,
        }

        public enum layersMask
        {
            // 열거형으로 bit flag 형태를 정의
            Default = 0 << 0, // 0 -> ... 00000000
            Ground  = 1 << 0, // 1 -> ... 00000001
            Player  = 1 << 1, // 2 -> ... 00000010
            Enemy   = 1 << 2, // 4 -> ... 00000100
        }

        /// <summary>
        /// 플레이어에 대한 정보
        /// </summary>
        class Player
        {
            // const 상수 키워드 
            // 해당 변수를 상수로서 사용하겠다고 명시하는 키워드.
            // 상수 취급이므로 런다팀 중에 값을 대입할 수 없다.
            public const int STATE_JUMP = 3;
            // 1 : Idle
            // 2 : Move
            // 3 : Jump
            // 4 : Fall
            // public int state;
            public State state;


        }
        static void Main(string[] args)
        {
            Player player = new Player();
            // player.state = 3; Magic Number..
            // player.state = Player.STATE_JUMP;
            player.state = State.Jump;

            // Enum class : 열거형 타입에 대한 편의 기능들을 제공하는 클래스
            // Type class : 어떤 타입을 대표하는 정보를 가질 수 있는 클래스
            // 어떤 타입을 Type class로 반환받고 싶을 때 typeof 함수를 사용한다.
            // Array Class : 배열에 대한 편의 기능들을 제공하는 클래스
            Array array = Enum.GetValues(typeof(State));
            // in 뒤 배열을 차례대로 접근하여 배열안의 데이터를 반환하는 구문
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }

            // switch/case 구문
            switch (player.state)
            {
                case State.None:
                    break;
                case State.Idle:
                    { }
                    // something to do when player is idle
                    break;
                case State.Move:
                    break;
                case State.Jump:
                    break;
                case State.Fall:
                    break;
                case State.Attack:
                    break;
                default:
                    break;
            }

            string names = string.Empty;
            switch (names)
            {
                case "철수":
                    break;
                case "영희":
                    break;
                default:
                    break;
            }

            int colliderLayer = 3;
            int colliderMask = 1 << 1 | 1 << 2;
            if((1 << colliderLayer & colliderMask) > 0)
            {

            }

            layersMask mask = layersMask.Player | layersMask.Enemy;
            if((1 << colliderLayer & (uint)mask) > 0)
            {

            }
            int[] arr = new int[array.Length];
            Enum.GetName(typeof(State), player.state);

            Console.WriteLine("Hello, World!");
        }
    }
}