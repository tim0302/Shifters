using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shifters
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;
        public bool b_Input;
        public bool r_Input;
        public bool rb_Input;
        public bool rt_Input;
        public bool rollFlag;
        public bool healFlag;
        public bool comboFlag;
        public bool lockOnInput;
        public bool lockOnFlag;
        public bool restartInput;
        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        CameraHandler cameraHandler;
        Vector2 movementInput;
        Vector2 cameraInput;
        UIManager uIManager;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            uIManager = GetComponent<UIManager>();
        }
        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += inputActions => cameraInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            HandleMoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleHealInput(delta);
            HandleLockOnInput();
            HandleRestartInput();
        }

        private void HandleRestartInput()
        {
            restartInput = inputActions.PlayerActions.Restart.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            if (restartInput == true)
            {
                SceneManager.LoadScene("Scene1");
            }
        }

        private void HandleLockOnInput()
        {
            if (lockOnInput && lockOnFlag == false)
            {
                cameraHandler.ClearLockOnTargets();
                lockOnInput = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                    uIManager.TurnOnLockOnIndicator();
                }
            }
            else if (lockOnInput && lockOnFlag)
            {
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
                uIManager.TurnOffLockOnIndicator();
            }

            cameraHandler.SetCameraHeight();
        }
        public void HandleHealInput(float delta)
        {
            r_Input = inputActions.PlayerActions.Heal.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            if (r_Input)
            {
                healFlag = true;
            }
        }
        public void HandleMoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            if (b_Input)
            {
                rollFlag = true;
            }
        }

        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.RB.performed += i => rb_Input = true;
            inputActions.PlayerActions.RT.performed += i => rt_Input = true;
            if (rb_Input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.weapon);
                    comboFlag = false;
                }

                else
                {
                    playerAttacker.HandleLightAttack(playerInventory.weapon);
                }

            }
            if (rt_Input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.weapon);
            }
        }
    }
}