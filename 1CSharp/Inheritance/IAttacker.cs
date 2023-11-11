using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal interface IAttacker
    {
        float attackPower { get; }
        float criticalGain { get; }

        void Attack(IHp target, bool isCritical);
    }
}
