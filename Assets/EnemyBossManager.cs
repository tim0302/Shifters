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
        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            enemyStats = GetComponent<EnemyStats>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }

        public void UpdateBossHealthBar(int currentHealth)
        {
            bossHealthBar.SetBossCurrentHealth(currentHealth);
        }
    }
}
