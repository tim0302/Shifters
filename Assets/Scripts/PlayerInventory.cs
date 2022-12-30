using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponManager weaponManager;
        public WeaponItem weapon;

        private void Awake()
        {
            weaponManager = GetComponentInChildren<WeaponManager>();
        }

        private void Start()
        {
            weaponManager.LoadWeapon(weapon);
        }
    }
}

