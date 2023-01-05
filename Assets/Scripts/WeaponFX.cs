using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("Weapon FX")]
        public ParticleSystem normalWeaponTrail;
        public ParticleSystem fireTrail;
        public ParticleSystem bloodTrail;
        public void PlayWeaponFX()
        {
            // normalWeaponTrail.Stop();

            // if (normalWeaponTrail.isStopped)
            // {
            normalWeaponTrail.Play();
            // }
        }

        public void PlayWeaponFX2()
        {
            fireTrail.Play();
        }
    }
}
