using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class RisingStorm : Ability, IEnemyAbility
    {
        public string abilityName => nameof(RisingStorm);
        public GameObject projectile;
        public float projectileSpeed;
        EnemyManager enemyManager;
        public float delay;

        private void Awake()
        {
            enemyManager = GetComponentInParent<EnemyManager>();
        }
        public void CastAbility()
        {
            Invoke(nameof(RisingStormCast), delay);

        }
        void RisingStormCast()
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - caster.transform.position;
            GameObject currentprojectile = Instantiate(projectile, caster.transform.position - new Vector3(0, 2, 0), Quaternion.identity);
            currentprojectile.transform.forward = direction.normalized;
            currentprojectile.GetComponent<Rigidbody>().AddForce(direction.normalized * projectileSpeed, ForceMode.Impulse);
        }
    }
}
