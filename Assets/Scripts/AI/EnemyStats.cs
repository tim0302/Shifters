using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class EnemyStats : CharacterStats
    {
        Animator animator;
        AnimatorHandler animatorHandler;
        EnemyBossManager enemyBossManager;
        CharacterSoundFXManager characterSoundFXManager;
        EnemyWeaponManager enemyWeaponManager;
        PlayerStats playerStats;
        void Awake()
        {
            playerStats = FindObjectOfType<PlayerStats>();
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            animator = GetComponentInChildren<Animator>();
            enemyWeaponManager = GetComponent<EnemyWeaponManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;
            playerStats.GainStaminaByDamage(damage);

            currentHealth -= damage;
            enemyBossManager.UpdateBossHealthBar(currentHealth);
            if (!animator.GetBool("isInteracting"))
            {
                animator.Play("Damage");
            }
            enemyWeaponManager.CloseDamageCollider();
            enemyWeaponManager.DisableEnemySpecialDamage();
            characterSoundFXManager.PlayRandomDamageSoundFX();
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death");
                characterSoundFXManager.PlayRandomDeathSound();
                isDead = true;
            }
        }

    }
}
