using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public interface IEnemyAbility
    {
        string abilityName { get; }
        void CastAbility();
    }
}

