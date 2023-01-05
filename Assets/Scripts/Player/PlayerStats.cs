using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shifters
{
    public class PlayerStats : CharacterStats
    {

        public HealthBar healthbar;
        public ManaBar manaBar;
        public Gradient gradient;
        Animator animator;
        AnimatorHandler animatorHandler;
        CharacterSoundFXManager characterSoundFXManager;
        WeaponManager weaponManager;
        PlayerManager playerManager;
        Image healthBar;
        public int playerSpecialAttackDamageMultiplier = 1;

        void Awake()
        {
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            animator = GetComponentInChildren<Animator>();

            weaponManager = GetComponent<WeaponManager>();
            playerManager = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            playerSpecialAttackDamageMultiplier = stamina / 10;
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
            manaBar.SetMaxMana(this.maxStamina);
            var barFill = GameObject.Find("Health Bar Fill");
            healthBar = barFill.GetComponent<Image>();
        }
        public void GainStaminaByDamage(int damage)
        {
            if ((stamina + damage / 2) <= maxStamina)
            {
                stamina += damage / 2;
                manaBar.SetCurrentMana(stamina);
            }
            else
            {
                stamina = maxStamina;
                manaBar.SetCurrentMana(stamina);

            }
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void Heal()
        {
            if (!animator.GetBool("isInteracting") && !animator.GetBool("canDoRollAttack"))
            {
                animatorHandler.PlayTargetAnimation("Heal", true);
                if (stamina == 0) { return; }

                if ((currentHealth + (stamina * maxHealth) / 600) <= maxHealth)
                {
                    currentHealth += (stamina * maxHealth) / 600;
                    healthBar.color = gradient.Evaluate((float)currentHealth / (float)maxHealth);

                }
                else
                {
                    currentHealth = maxHealth;
                    healthBar.color = gradient.Evaluate((float)currentHealth / (float)maxHealth);
                }

                healthbar.SetCurrentHealth(currentHealth);
                stamina = 0;
                manaBar.SetCurrentMana(stamina);

            }
        }

        public void TakeDamage(int damage)
        {


            if (playerManager.isInvulnerable)
                return;

            if ((stamina + damage / 4) <= maxStamina)
            {
                stamina += damage / 4;
                manaBar.SetCurrentMana(stamina);
            }
            else
            {
                stamina = maxStamina;
                manaBar.SetCurrentMana(stamina);

            }

            characterSoundFXManager.PlayRandomDamageSoundFX();
            weaponManager.CloseDamageCollider();
            weaponManager.DisableSpecialDamage();

            if (isDead)
                return;
            currentHealth -= damage;
            //change color
            healthBar.color = gradient.Evaluate((float)currentHealth / (float)maxHealth);

            healthbar.SetCurrentHealth(currentHealth);

            if (!animator.GetBool("isInteracting"))
            {
                if (damage >= 50)
                {
                    animatorHandler.PlayTargetAnimation("Stunned", true);
                }
                else
                {
                    animator.Play("Damage");
                }
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death", true);
                characterSoundFXManager.PlayRandomDeathSound();
                isDead = true;
            }
        }

    }
}
