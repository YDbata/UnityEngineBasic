namespace Inheritance
{
    internal abstract class PlayerbleCharacter : IHp, IAttacker
    {
        public float hpValue
        {
            get => _hp;
            private set
            {
                if (value == _hp)
                    return;
                if (value > hpMax)
                    value = hpMax;

                else if (value < hpMin)
                    value = hpMin;

                _hp = value;
            }
        }
        public float hpMax { get => hpMax; }
        /*        public float hp => throw new NotImplementedException();

                public float hpMax => throw new NotImplementedException();*/

        public float hpMin { get => _hpMin;}
        

        public float attackPower { get => _attackPower; }
        public float criticalGain { get => _criticalGain; }

        private float _hp;
        private float _hpMin;
        private float _attackPower;
        private float _criticalGain;

        // abstract에서 미리 default를 구현하고 싶을 때
        public virtual void Breath() {
            Console.WriteLine("Breath in");
            Console.WriteLine("Breath out");
            return; }
        public abstract void Move();

        public void Attack(IHp target, bool isCritical)
        {

            //target.DepleteHp(this, isCritical ? _attackPower * criticalGain : _attackPower);
            if (target is PlayerbleCharacter)
            {
                ((PlayerbleCharacter)target).DepleteHp<PlayerbleCharacter>(this, isCritical ? _attackPower * criticalGain : _attackPower);
            }
        }

        // Generic Type
        // 타입을 미리 정의해놓지 않고 특정 타입에 따라 다른 정의를 할 수 있도록
        // 형태만 정의
        // 특징은 컴파일타임에 특정 타입을 명시해 놓고 이 Generic Type을 쓰는 코드가 있으면
        // 컴파일러가 그 정의를 따라 만들어서 빌드에 추가
        void DepleteHp<T>(T subject, float amount)
        {
            hpValue -= amount;
        }

        public void DepleteHp(object subject, float amount)
        {
            hpValue -= amount;
        }

        public void RecoverHp(object subject, float amount)
        {
            hpValue += amount;
        }
    }
}
