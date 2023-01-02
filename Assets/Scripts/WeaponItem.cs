using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;

        [Header("Attack Animations")]
        public string Light_Attack_1;

        public string Light_Attack_2;
        public string Heavy_Attack;

        public string Special_Attack;
        [Header("Sound FX")]
        public AudioClip[] weaponWhooshes;

    }
}


