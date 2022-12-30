using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class EnemyStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;
        Animator animator;
        AnimatorHandler animatorHandler;

        CharacterSoundFXManager characterSoundFXManager;

        void Awake()
        {
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            animator = GetComponentInChildren<Animator>();
        }
        void Start()
        {
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
            if (currentHealth <= 0)
            {
                return;
            }
            currentHealth -= damage;
            animator.Play("Damage");

            characterSoundFXManager.PlayRandomDamageSoundFX();
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death");
                characterSoundFXManager.PlayRandomDeathSound();
            }
        }

    }
}
