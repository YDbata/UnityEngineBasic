namespace Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PlayerbleCharacter character = new PlayerbleCharacter(); 추상클래스는 만들수 없다
            SwordMan swordMan = new SwordMan();
            // 공변성(Covariant)
            // 하위타입 객체를 기반타입으로 참조할 수 있는 성질
            // 객체가 할당될때, 기반타입의 데이터부터 차례대로 할당을 하기 때문에 가능
            PlayerbleCharacter swordMan1 = new SwordMan();
            PlayerbleCharacter wizard = new Wizard();
            PlayerbleCharacter intermidiateWizard = new intermidiateWizard();

            swordMan.Move();
            swordMan1.Move();
            wizard.Move();
            intermidiateWizard.Move(); // Wizard의 Move

            // 가상함수 table 설명
            swordMan.SwordMasteryLevel = 1;
            swordMan.Smash();
            // sowrdMan1은 PlatyerbleCharacter로 생성했기 때문에 sowrdMan class의 변수는 읽을 수 없다.(함수 포함)
            //swordMan1.SwordMasteryLevel;
            //swordMan1.Smash();

            // object : C#의 모든 타입의 기반 타입

            // Boxing : object type 처리하는 방식
            // object type변수에 데이터를 대입하면 heap memory에 새로운 객체를 생성
            // TypeInfo(데이터의 타입 참조테이블인덱스)와 데이터를 씀.
            object int1 = 3; 
            object str = "zㅣ존검사";
            object wizard2 = new Wizard();

            // unboxing ; object 객체에서 원래 데이터를 읽어오는 과정(명시적 캐스팅 필요)
            int a = (int)int1;
        }
    }
}