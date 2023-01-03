using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class WeaponManager : MonoBehaviour
    {
        WeaponHolder weaponHolder;
        CharacterEffectManager characterEffectManager;
        DamageCollider damageCollider;
        WeaponItem weaponItem;
        CharacterSoundFXManager characterSoundFXManager;
        private void Awake()
        {
            WeaponHolder weaponHolder = GetComponentInChildren<WeaponHolder>();
            this.weaponHolder = weaponHolder;
            characterEffectManager = GetComponent<CharacterEffectManager>();
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            weaponItem = GetComponentInChildren<WeaponItem>();
        }

        private void Start()
        {

        }

        public void LoadWeapon(WeaponItem weaponItem)
        {
            weaponHolder.LoadWeaponModel(weaponItem);
            LoadWeaponDamageCollider();
        }

        private void LoadWeaponDamageCollider()
        {
            damageCollider = weaponHolder.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            characterEffectManager.weaponFX = weaponHolder.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            damageCollider.currentWeaponDamage = weaponItem.baseDamage;
        }

        public void OpenDamageCollider()
        {
            characterSoundFXManager.PlayRandomWeaponWhoosh();
            damageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider()
        {
            damageCollider.DisableDamageCollider();
        }

        public void EnableSpecialDamage()
        {
            damageCollider.EnableSpecialDamage();
        }
        public void DisableSpecialDamage()
        {
            damageCollider.DisableSpecialDamage();
        }

    }
}

