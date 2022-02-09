using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aiming;
		public bool isPullingBack;
		public bool hasReleased;
		public bool isFP;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

        private void Update()
        {
			if (Shoot.canFire == false)
			{
				isPullingBack = false;
				hasReleased = false;
			}
		}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void Move(InputAction.CallbackContext context)
		{
			MoveInput(context.ReadValue<Vector2>());
		}

		public void Look(InputAction.CallbackContext context)
		{
			if(cursorInputForLook)
			{
				LookInput(context.ReadValue<Vector2>());
			}
		}

		public void Jump(InputAction.CallbackContext context)
		{
			JumpInput(context.ReadValueAsButton());
		}

		public void Sprint(InputAction.CallbackContext context)
		{
			SprintInput(context.ReadValueAsButton());
		}

		public void Aim(InputAction.CallbackContext context)
		{
			AimInput(context.ReadValueAsButton());
		}

		public void Attack(InputAction.CallbackContext context)
		{
			AttackInput(context.ReadValueAsButton());
		}

		public void Switch(InputAction.CallbackContext context)
		{
			SwitchInput();
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		// sets reticle and updates view on aim
		public void AimInput(bool isDrawn)
		{
			aiming = isDrawn;
		}

		public void AttackInput(bool isPulling)
        {
			isPullingBack = isPulling;
		}

		// switches to first person view/ vice-versa
		public void SwitchInput()
		{
			CameraManager.Instance.switchViews();
			isFP = !isFP;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}