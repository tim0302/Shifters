using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class FlameCutEnd : Ability, IEnemyAbility
    {
        public Collider dk;
        public float delay;
        public string abilityName => nameof(FlameCutEnd);
        public float abilityRange;
        public LayerMask isEnemy;

        public void CastAbility()
        {
            Invoke(nameof(FlameCutEndCast), delay);

        }
        void FlameCutEndCast()
        {
            GameObject currentAttack = Instantiate(visualEffect, caster.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

            currentAttack.transform.forward = caster.transform.forward;
            Collider[] enemies = Physics.OverlapSphere(transform.position, abilityRange, isEnemy);
            for (int i = 0; i < enemies.Length; i++)
            {
                PlayerStats characterStats = enemies[i].transform.GetComponent<PlayerStats>();
                characterStats.TakeDamage(damage);
            }
        }
    }
}
