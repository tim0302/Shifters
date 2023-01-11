using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class EnemyBossManager : MonoBehaviour
    {
        UIBossHealthBar bossHealthBar;
        public string bossName;
        EnemyStats enemyStats;
        EnemyAnimatorManager enemyAnimatorManager;
        BossCombatStanceState bossCombatStanceState;
        public bool isSecondPhase;


        [Header("Second Phase FX")]
        public GameObject phaseChangeFX;
        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            enemyStats = GetComponent<EnemyStats>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }

        public void UpdateBossHealthBar(int currentHealth, int maxHealth)
        {
            bossHealthBar.SetBossCurrentHealth(currentHealth);
            if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted)
            {
                bossCombatStanceState.hasPhaseShifted = true;
                ShiftToSecondPhase();
            }
        }

        public void ShiftToSecondPhase()
        {
            isSecondPhase = true;
            enemyAnimatorManager.animator.SetBool("isInvulnerable", true);
            enemyAnimatorManager.animator.SetBool("isPhaseShifting", true);
            enemyAnimatorManager.PlayTargetAnimation("Heal", true);
            bossCombatStanceState.hasPhaseShifted = true;
        }
    }
}
