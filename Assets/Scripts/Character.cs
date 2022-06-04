using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
	public void ApplyDamage(float damageValue)
	{
		health -= 
			characterControl.Blocking && !isAttacking ?
			damageValue :
			damageValue / 2;
	}

	[SerializeField]
	CharacterController controller;
	[SerializeField]
	Animator animator;
	[SerializeField]
	CharacterControl characterControl;
	[SerializeField]
	Damager blade;

	[SerializeField]
	float health = 100;
	[SerializeField]
	float movingSpeed = 5;
	[SerializeField]
	float attackDuration = 1;


	float verticalVelocity;
	bool isAttacking
	{
		get => blade.Enabled;
		set => blade.Enabled = value;
	}

	private void Update()
	{
		verticalVelocity =
			controller.velocity.y +
			(controller.isGrounded ? 0f : -9.8f * Time.deltaTime);
		if (characterControl.NeedToPerformAttack) Attack();
		Vector2 horizontalMovementInput =
			!isAttacking && !characterControl.Blocking ?
			characterControl.MoveHorizontal : Vector2.zero;
		animator.SetBool("Running", horizontalMovementInput != Vector2.zero);
		animator.SetBool("Blocking", characterControl.Blocking);
		controller.
			SimpleMove(
				CurrentMovement(
					InputMovementFromHorizontal(horizontalMovementInput)));
		transform.rotation = 
			Quaternion.Euler(0, characterControl.Facing.eulerAngles.y, 0);
	}

	private void Attack()
	{
		animator.SetTrigger("Attack");
		isAttacking = true;
		StartCoroutine(AttackingStateExitCoroutine());
	}

	Vector3 CurrentMovement(Vector3 inputMovement) =>
		inputMovement * movingSpeed +
		Vector3.up * verticalVelocity;

	
	Vector3 InputMovementFromHorizontal(Vector2 moveInputHorizontal) =>
		transform.right * moveInputHorizontal.x +
		transform.forward * moveInputHorizontal.y;

	IEnumerator AttackingStateExitCoroutine()
	{
		yield return new WaitForSeconds(attackDuration);
		isAttacking = false;
	}
}
