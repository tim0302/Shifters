using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class WindSlash : Ability, IEnemyAbility
    {
        public string abilityName => nameof(WindSlash);
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
            Invoke(nameof(WindSlashCast), delay);

        }
        void WindSlashCast()
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - caster.transform.position;
            GameObject currentprojectile = Instantiate(projectile, caster.transform.position, Quaternion.identity);
            currentprojectile.transform.forward = direction.normalized;
            currentprojectile.GetComponent<Rigidbody>().AddForce(direction.normalized * projectileSpeed, ForceMode.Impulse);
        }
    }
}
