using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class EnemyAbilityHandler : MonoBehaviour
    {
        public void CastAbility(AbilitiesEnum ability)
        {
            switch (ability)
            {
                case AbilitiesEnum.FlameCutEnd:
                    FlameCutEnd rua = GetComponentInChildren<FlameCutEnd>();
                    rua.CastAbility();
                    break;
                case AbilitiesEnum.WindSlash:
                    WindSlash slash = GetComponentInChildren<WindSlash>();
                    slash.CastAbility();
                    break;
                case AbilitiesEnum.RisingStorm:
                    RisingStorm storm = GetComponentInChildren<RisingStorm>();
                    storm.CastAbility();
                    break;

                default:
                    break;
            }
        }
    }
}
