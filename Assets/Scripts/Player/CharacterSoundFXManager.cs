using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        //attack grundts
        //take damage grunnts
        [Header("Taking Damage Sounds")]
        public AudioClip[] takingDamageSounds;
        private List<AudioClip> potentialDamageSounds;
        private AudioClip lastDamageSoundsPlayed;


        [Header("Enemy Weapon Whooshes")]
        public AudioClip[] weaponWhooshesSoundEnemy;
        private List<AudioClip> potentialWeaponWhooshesEnemy;
        private AudioClip lastWeaponWhooshEnemy;

        [Header("Weapon Whooshes")]
        // public AudioClip[]
        private List<AudioClip> potentialWeaponWhooshes;
        private AudioClip lastWeaponWhoosh;
        //foot step
        AudioSource audioSource;
        PlayerInventory playerInventory;

        [Header("Character Death")]
        public AudioClip[] deathSounds;

        public AudioClip endGame;
        [Header("FireSlash")]
        public AudioClip[] fireSlash;
        private List<AudioClip> potentialFireSlashSound;
        private AudioClip lastFireSlashSound;

        [Header("Weapon collision")]
        private List<AudioClip> potentialWeaponCollisionSound;
        public AudioClip[] weaponCollisionSound;
        private AudioClip lastWeaponCollisionSound;

        [Header("FireSlash hit")]
        private List<AudioClip> potentialFlameSlashHitSound;
        public AudioClip[] flameSlashHitSound;
        private AudioClip lastFlameSlashHitSound;
        public AudioClip rollSound;
        public AudioClip footStep;
        public AudioClip dragonRoar;
        public AudioClip heal;
        public AudioClip flameCutEnd;
        public AudioClip bladeParrySound;
        AnimatorHandler animatorHandler;
        protected void Awake()
        {
            animatorHandler = GetComponent<AnimatorHandler>();
            audioSource = GetComponent<AudioSource>();
            playerInventory = GetComponent<PlayerInventory>();
        }
        public virtual void PlayRandomDamageSoundFX(int damage)
        {
            potentialDamageSounds = new List<AudioClip>();
            foreach (var damageSound in takingDamageSounds)
            {
                if (damageSound != lastDamageSoundsPlayed)
                {
                    potentialDamageSounds.Add(damageSound);
                }
            }

            int randomValue = Random.Range(0, potentialDamageSounds.Count);
            lastDamageSoundsPlayed = potentialDamageSounds[randomValue];
            audioSource.PlayOneShot(potentialDamageSounds[randomValue], damage / 50f);

        }

        public virtual void PlayRandomWeaponWhoosh()
        {
            potentialWeaponWhooshes = new List<AudioClip>();
            foreach (var whooshSound in playerInventory.weapon.weaponWhooshes)
            {
                if (whooshSound != lastWeaponWhoosh)
                {
                    potentialWeaponWhooshes.Add(whooshSound);
                }
            }
            int randomValue = Random.Range(0, potentialWeaponWhooshes.Count);
            lastWeaponWhoosh = potentialWeaponWhooshes[randomValue];
            audioSource.PlayOneShot(potentialWeaponWhooshes[randomValue]);
        }

        public void PlayBladeParrySound()
        {
            potentialWeaponCollisionSound = new List<AudioClip>();
            foreach (var collisionSound in weaponCollisionSound)
            {
                if (collisionSound != lastWeaponCollisionSound)
                {
                    potentialWeaponCollisionSound.Add(collisionSound);
                }
            }
            int randomValue = Random.Range(0, potentialWeaponCollisionSound.Count);
            lastWeaponCollisionSound = potentialWeaponCollisionSound[randomValue];
            audioSource.PlayOneShot(potentialWeaponCollisionSound[randomValue], 2.0f);
        }

        public virtual void PlayRandomEnemyWeaponWhoosh()
        {
            potentialWeaponWhooshesEnemy = new List<AudioClip>();
            foreach (var whooshSound in weaponWhooshesSoundEnemy)
            {
                if (whooshSound != lastWeaponWhooshEnemy)
                {
                    potentialWeaponWhooshesEnemy.Add(whooshSound);
                }
            }
            int randomValue = Random.Range(0, potentialWeaponWhooshesEnemy.Count);
            lastWeaponWhooshEnemy = potentialWeaponWhooshesEnemy[randomValue];
            audioSource.PlayOneShot(potentialWeaponWhooshesEnemy[randomValue], 2.0f);
        }

        public virtual void PlayRandomDeathSound()
        {
            int randomValue = Random.Range(0, deathSounds.Length);
            audioSource.PlayOneShot(deathSounds[randomValue]);
            PlayEndGameSound();
        }

        public virtual void PlayEndGameSound()
        {
            audioSource.PlayOneShot(endGame);
        }
        public virtual void PlayFireSlash()
        {
            potentialFireSlashSound = new List<AudioClip>();
            foreach (var fireSlashSound in fireSlash)
            {
                if (fireSlashSound != lastFireSlashSound)
                {
                    potentialFireSlashSound.Add(fireSlashSound);
                }
            }
            int randomValue = Random.Range(0, potentialFireSlashSound.Count);
            lastFireSlashSound = potentialFireSlashSound[randomValue];
            audioSource.PlayOneShot(potentialFireSlashSound[randomValue], 2.0f);
            audioSource.PlayOneShot(dragonRoar);
        }

        public virtual void PlayRollSound()
        {
            audioSource.PlayOneShot(rollSound, 0.35f);
        }

        public virtual void PlayHeal()
        {
            audioSource.PlayOneShot(heal, 0.5f);
        }

        public virtual void PlayFlameCutEnd()
        {
            audioSource.PlayOneShot(flameCutEnd, 1);
        }

        public virtual void PlayFootStep()
        {
            if (!animatorHandler.animator.GetBool("isInteracting"))
            {
                audioSource.PlayOneShot(footStep, 0.05f);
            }
        }
        public virtual void PlayFireSlashHitSound()
        {
            potentialFlameSlashHitSound = new List<AudioClip>();
            foreach (var hitSound in flameSlashHitSound)
            {
                if (hitSound != lastFlameSlashHitSound)
                {
                    potentialFlameSlashHitSound.Add(hitSound);
                }
            }

            int randomValue = Random.Range(0, potentialFlameSlashHitSound.Count);
            lastFlameSlashHitSound = potentialFlameSlashHitSound[randomValue];
            audioSource.PlayOneShot(potentialFlameSlashHitSound[randomValue]);
        }
    }
}
