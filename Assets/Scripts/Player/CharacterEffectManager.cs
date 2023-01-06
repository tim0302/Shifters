using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class CharacterEffectManager : MonoBehaviour
    {
        [Header("Damage FX")]
        public GameObject onHitFX;
        [Header("Heal FX")]
        public GameObject healFX;
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

        public virtual void PlayOnHitFX(Vector3 hitLocation)
        {
            GameObject onHit = Instantiate(onHitFX, hitLocation, Quaternion.identity);
        }

        public virtual void PlayHealFX(Vector3 playerLocation)
        {
            GameObject heal = Instantiate(healFX, playerLocation, Quaternion.identity);
        }
    }
}
