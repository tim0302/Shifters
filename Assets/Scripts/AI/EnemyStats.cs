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
        EnemyManager enemyManager;
        EnemyWeaponManager enemyWeaponManager;
        PlayerStats playerStats;
        EnemyAttackAction enemyAttackAction;

        void Awake()
        {
            playerStats = FindObjectOfType<PlayerStats>();
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            animator = GetComponentInChildren<Animator>();
            enemyWeaponManager = GetComponent<EnemyWeaponManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
            enemyManager = GetComponent<EnemyManager>();
            currentHealth = maxHealth;
            enemyAttackAction = FindObjectOfType<EnemyAttackAction>();
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void WeaponCollision()
        {
            if (enemyManager.isPhaseShifting)
            {
                return;
            }
            enemyManager.currentRecoveryTime = 5f;
            animator.SetBool("isInteracting", true);
            // animator.Play("Stunned");
            characterSoundFXManager.PlayBladeParrySound();
            playerStats.GainStaminaByParry(25);
            // animator.Play("Damage");
            enemyWeaponManager.CloseDamageCollider();
            enemyWeaponManager.DisableEnemySpecialDamage();
            enemyManager.ResetRecoveryTime();
        }

        public void TakeDamage(int damage)
        {
            if (isDead || enemyManager.isPhaseShifting)
                return;

            currentHealth -= damage;
            enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);

            if (!animator.GetBool("isInteracting"))
            {
                animator.Play("Damage");
            }
            enemyWeaponManager.CloseDamageCollider();
            enemyWeaponManager.DisableEnemySpecialDamage();
            characterSoundFXManager.PlayRandomDamageSoundFX(damage);
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
