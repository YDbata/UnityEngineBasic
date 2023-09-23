using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // interface : 기능을 추상화 하는 사용자 정의 자료형
    // naming : 대문자 I + PascalCase
    internal interface IHp
    {
        float hpValue { get; }
        float hpMax { get; }
        float hpMin { get; }

        // object : C#의 모든 타입의 기반 타입

        
        void DepleteHp(object subject, float amount);
        void RecoverHp(object subject, float amount);

    }
}
