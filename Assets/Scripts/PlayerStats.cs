using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;
        public HealthBar healthbar;
        AnimatorHandler animatorHandler;
        CharacterSoundFXManager characterSoundFXManager;

        void Awake()
        {
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthbar.SetCurrentHealth(currentHealth);
            animatorHandler.PlayerTargetAnimation("Damage", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayerTargetAnimation("Death", true);
                characterSoundFXManager.PlayRandomDeathSound();
            }
        }

    }
}
