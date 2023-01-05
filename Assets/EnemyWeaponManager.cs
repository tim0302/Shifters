using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class EnemyWeaponManager : MonoBehaviour
    {
        public WeaponItem weapon;
        WeaponHolder weaponHolder;
        DamageCollider damageCollider;
        CharacterSoundFXManager characterSoundFXManager;
        CharacterEffectManager characterEffectManager;
        private void Awake()
        {
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            WeaponHolder weaponHolder = GetComponentInChildren<WeaponHolder>();
            characterEffectManager = GetComponent<CharacterEffectManager>();
            this.weaponHolder = weaponHolder;
        }
        private void Start()
        {
            LoadWeapon(weapon);
        }
        public void LoadWeapon(WeaponItem weapon)
        {
            weaponHolder.LoadWeaponModel(weapon);
            LoadWeaponDamageCollider();
        }

        public void LoadWeaponDamageCollider()
        {
            damageCollider = weaponHolder.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            characterEffectManager.weaponFX = weaponHolder.currentWeaponModel.GetComponentInChildren<WeaponFX>();
        }

        public void OpenDamageCollider()
        {
            characterSoundFXManager.PlayRandomEnemyWeaponWhoosh();
            // characterEffectManager.PlayWeaponFX(false);
            damageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider()
        {
            damageCollider.DisableDamageCollider();
        }


        public void EnableEnemySpecialDamage()
        {
            damageCollider.EnableEnemySpecialDamage();
        }
        public void DisableEnemySpecialDamage()
        {
            damageCollider.DisableEnemySpecialDamage();
        }
    }
}
