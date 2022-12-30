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
        public CharacterSoundFXManager characterSoundFXManager;

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
            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }
        //to reset the flags
        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
        }
    }
}
