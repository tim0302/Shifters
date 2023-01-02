using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class CharacterEffectManager : MonoBehaviour
    {
        public WeaponFX weaponFX;

        public virtual void PlayWeaponFX(bool isSpecial)
        {
            if (isSpecial)
            {
                weaponFX.PlayWeaponFX2();
            }
            else
            {
                weaponFX.PlayWeaponFX();
            }
        }
    }
}
