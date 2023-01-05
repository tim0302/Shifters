using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shifters
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator animator;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        public bool isInteracting;
        public bool canDoCombo;
        public bool canDoRollAttack;
        public CharacterSoundFXManager characterSoundFXManager;
        public bool isInvulnerable;
        void Awake()
        {
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
        }
        // [Header("Player Flags")]
        void Start()
        {
            Application.targetFrameRate = 144;
            cameraHandler = CameraHandler.singleton;
            inputHandler = GetComponent<InputHandler>();
            animator = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = animator.GetBool("isInteracting");
            canDoCombo = animator.GetBool("canDoCombo");
            canDoRollAttack = animator.GetBool("canDoRollAttack");
            inputHandler.TickInput(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleHealing(delta);
            isInvulnerable = animator.GetBool("isInvulnerable");
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            if (!isInteracting || canDoRollAttack)
            {
                playerLocomotion.HandleMovement(delta);
            }

        }
        //to reset the flags
        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.healFlag = false;

            float delta = Time.deltaTime;
            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }
    }
}
