using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : CharacterControl
{
	[SerializeField]
	Transform cameraTransform;
	[SerializeField]
	private float maxRotationSpeed = 10;

	Transform CameraTransform =>
		cameraTransform != null ? 
		cameraTransform : (cameraTransform = Camera.main.transform);

	
	PlayerInput inputActions;
	PlayerInput InputActions => 
		inputActions != null ? inputActions : (inputActions = new PlayerInput());

	public override Vector2 MoveHorizontal
	{
		get
		{
			Vector2 horMove = InputActions.Player.Move.ReadValue<Vector2>();
			if (horMove != Vector2.zero) UpdateFacingTarget();
			return horMove;
		}
	}

	public override bool NeedToPerformAttack
	{
		get
		{
			if (InputActions.Player.Fire.triggered)
			{
				UpdateFacingTarget();
				return true;
			}
			return false;
		}
	}

	public override bool Blocking
	{
		get
		{
			if (InputActions.Player.Block.IsPressed())
			{
				UpdateFacingTarget();
				return true;
			}
			return false;
		}
	}

	Quaternion facingTarget = Quaternion.identity;

	Quaternion facing = Quaternion.identity;

	public override Quaternion Facing =>
		facing == facingTarget ?
		facing :
		(facing = Quaternion.RotateTowards(
			facing, 
			facingTarget, 
			maxRotationSpeed));

	private void UpdateFacingTarget()
	{
		facingTarget = CameraTransform.rotation;
	}

	private void OnEnable()
	{
		InputActions.Player.Enable();
	}
	private void OnDisable()
	{
		InputActions.Player.Disable();
	}
}
