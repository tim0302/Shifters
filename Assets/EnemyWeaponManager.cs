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

        private void Awake()
        {
            WeaponHolder weaponHolder = GetComponentInChildren<WeaponHolder>();
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
        }

        public void OpenDamageCollider()
        {
            damageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider()
        {
            damageCollider.DisableDamageCollider();
        }
    }
}
