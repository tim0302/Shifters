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
        public AudioClip flameSlash;

        [Header("FireSlash hit")]
        private List<AudioClip> potentialFlameSlashHitSound;
        public AudioClip[] flameSlashHitSound;
        private AudioClip lastFlameSlashHitSound;
        public AudioClip rollSound;
        public AudioClip footStep;
        public AudioClip dragonRoar;
        AnimatorHandler animatorHandler;
        protected void Awake()
        {
            animatorHandler = GetComponent<AnimatorHandler>();
            audioSource = GetComponent<AudioSource>();
            playerInventory = GetComponent<PlayerInventory>();
        }
        public virtual void PlayRandomDamageSoundFX()
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
            audioSource.PlayOneShot(potentialDamageSounds[randomValue], 0.2f);

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
            audioSource.PlayOneShot(flameSlash);
            audioSource.PlayOneShot(dragonRoar);
        }

        public virtual void PlayRollSound()
        {
            audioSource.PlayOneShot(rollSound, 0.35f);
        }

        public virtual void PlayFootStep()
        {
            if (!animatorHandler.animator.GetBool("isInteracting"))
            {
                audioSource.PlayOneShot(footStep, 0.1f);
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
