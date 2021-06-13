using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[System.Serializable]
public class InputEventVector2 : UnityEvent<float, float> { }

[System.Serializable]
public class InputEventBool : UnityEvent<bool> { }


namespace NWW
{
    public sealed class InputManager : MonoBehaviour
    {
        private PlayerActions controls;
        public InputEventVector2 moveInputEvent;
        public InputEventBool runInputEvent;
        public InputEventBool aimInputEvent;
        public InputEventBool attackInputEvent;
        public InputEventBool switchInputEvent;
        public InputEventBool jumpInputEvent;

        private bool isRunning, isAiming, hasPulledBack;

        // Anim Behavior
        public PlayerAnimationBehaviour playerAnimationBehaviour;

        private void Awake()
        {
            controls = new PlayerActions();
            playerAnimationBehaviour.SetupBehaviour();
        }

        private void OnEnable()
        {
            controls.PlayerControls.Enable();
            controls.PlayerControls.Movement.performed += OnMove;
            controls.PlayerControls.Movement.canceled += OnMove;
            controls.PlayerControls.Run.performed += OnRun;
            controls.PlayerControls.Run.canceled += OnRun;
            controls.PlayerControls.Aim.performed += OnAim;
            controls.PlayerControls.Aim.canceled += OnAim;
            controls.PlayerControls.Switch.performed += OnSwitch;
            controls.PlayerControls.Switch.canceled += OnSwitch;
            controls.PlayerControls.Jump.performed += OnJump;
            controls.PlayerControls.Jump.canceled += OnJump;
            controls.PlayerControls.Attack.performed += OnAttack;
            controls.PlayerControls.Attack.canceled += OnAttack;
        }

        void OnDestroy()
        {
            controls.PlayerControls.Disable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            moveInputEvent.Invoke(moveInput.x, moveInput.y);
            playerAnimationBehaviour.UpdateHorzMovementAnimation(moveInput.x);
            playerAnimationBehaviour.UpdateVertMovementAnimation(moveInput.y);
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            bool runInput = context.ReadValueAsButton();
            isRunning = runInput;
            if(isAiming == false)
            {
                runInputEvent.Invoke(runInput);
                playerAnimationBehaviour.UpdateRunAnimation(runInput);
            }
            
        }

        private void OnAim(InputAction.CallbackContext context)
        {
            bool aimInput = context.ReadValueAsButton();
            isAiming = aimInput;
            if (isRunning == false)
            {
                aimInputEvent.Invoke(aimInput);
                playerAnimationBehaviour.UpdateAimAnimation(aimInput);
            }
            
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            bool attackInput = context.ReadValueAsButton();
            
            if (isAiming == true)
            {
                if (attackInput == true && Shoot.canFire == true)
                {
                    hasPulledBack = true;
                    attackInputEvent.Invoke(attackInput);
                    playerAnimationBehaviour.UpdateShotAnimation(!attackInput);
                } else if (attackInput == false && hasPulledBack == true)
                {
                    attackInputEvent.Invoke(attackInput);
                    playerAnimationBehaviour.UpdateShotAnimation(!attackInput);
                    hasPulledBack = false;
                } else
                {
                    playerAnimationBehaviour.UpdateShotAnimation(false);
                }
                
            }

        }

        private void OnSwitch(InputAction.CallbackContext context)
        {
            bool switchInput = context.ReadValueAsButton();
            switchInputEvent.Invoke(switchInput);
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            bool jumpInput = context.ReadValueAsButton();
            jumpInputEvent.Invoke(jumpInput);
            playerAnimationBehaviour.UpdateJumpAnimation(jumpInput);
        }
    }
}