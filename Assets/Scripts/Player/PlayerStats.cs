using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shifters
{
    public class PlayerStats : CharacterStats
    {

        public HealthBar healthbar;
        public Gradient gradient;
        AnimatorHandler animatorHandler;
        CharacterSoundFXManager characterSoundFXManager;
        WeaponManager weaponManager;
        PlayerManager playerManager;
        void Awake()
        {
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            weaponManager = GetComponent<WeaponManager>();
            playerManager = GetComponent<PlayerManager>();
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
            if (playerManager.isInvulnerable)
                return;
            if (isDead)
                return;
            currentHealth -= damage;
            //change color
            var barFill = GameObject.Find("Health Bar Fill");
            Image image = barFill.GetComponent<Image>();
            image.color = gradient.Evaluate((float)currentHealth / (float)maxHealth);

            healthbar.SetCurrentHealth(currentHealth);
            animatorHandler.PlayerTargetAnimation("Damage", true);
            characterSoundFXManager.PlayRandomDamageSoundFX();
            weaponManager.CloseDamageCollider();
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayerTargetAnimation("Death", true);
                characterSoundFXManager.PlayRandomDeathSound();
                isDead = true;
            }
        }

    }
}
