using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour, IDamagable
{
	[SerializeField]
	CharacterController controller;
	[SerializeField]
	Animator animator;
	[SerializeField]
	CharacterControl characterControl;
	[SerializeField]
	Damager blade;
	[SerializeField]
	MonoBehaviour healthBar;

	[Space]
	[SerializeField]
	float maxHealth = 100;
	[SerializeField]
	float attackValue = 10;
	[SerializeField]
	float movingSpeed = 5;
	[SerializeField]
	float swingDurationBeforeHit = 0.5f;
	[SerializeField]
	float hitDuration = 0.5f;
	IHealthBar HealthBar =>
		(IHealthBar)((IHealthBar)healthBar != null ?
		healthBar :
		healthBar = 
		(healthBar?.GetComponent<IHealthBar>() ?? 
		GetComponentInChildren<IHealthBar>())
		as MonoBehaviour);

	float verticalVelocity;
	bool isAttacking
	{
		get => blade.Enabled;
		set => blade.Enabled = value;
	}
	bool isDying = false;
	float Health
	{
		get => HealthBar.Value;
		set => HealthBar.Value = value;
	}

	private void Start()
	{
		HealthBar.MaxValue = maxHealth;
		HealthBar.Value = maxHealth;
	}

	private void Update()
	{
		UpdateVerticalVelocity();
		if (characterControl?.NeedToPerformAttack ?? false) Attack();
		Vector2 horizontalMovementInput =
			!isAttacking && !characterControl.Blocking ?
			characterControl?.MoveHorizontal ?? Vector2.zero : 
			Vector2.zero;
		ApplyCurrentMovementToController(horizontalMovementInput);
		transform.rotation = 
			Quaternion.Euler(0, characterControl?.Facing.eulerAngles.y ?? 0f, 0);
		animator?.SetBool("Running", horizontalMovementInput != Vector2.zero);
		animator?.SetBool("Blocking", characterControl?.Blocking ?? false);
	}
	void UpdateVerticalVelocity()
	{
		if(controller != null)
			verticalVelocity =
			controller.velocity.y +
			(controller.isGrounded ? 0f : -9.8f * Time.deltaTime);
	}
	void ApplyCurrentMovementToController(Vector2 horizontalMovementInput)
	{
		if (controller != null)
			controller.
				SimpleMove(
					CurrentMovement(
						InputMovementFromHorizontal(horizontalMovementInput)));
	}

	Vector3 CurrentMovement(Vector3 inputMovement) =>
		inputMovement * movingSpeed +
		Vector3.up * verticalVelocity;

	Vector3 InputMovementFromHorizontal(Vector2 moveInputHorizontal) =>
		transform.right * moveInputHorizontal.x +
		transform.forward * moveInputHorizontal.y;

	private void Attack()
	{
		animator.SetTrigger("Attack");
		blade.DamageValue = attackValue;
		StartCoroutine(AttackingStateChangingCoroutine());
	}
	IEnumerator AttackingStateChangingCoroutine()
	{
		yield return new WaitForSeconds(swingDurationBeforeHit);
		isAttacking = true;
		yield return new WaitForSeconds(hitDuration);
		isAttacking = false;
	}

	public void TakeDamage(IDamager damager)
	{
		if (damager == (IDamager)blade) return;
		Health -= 
			characterControl.Blocking && !isAttacking ?
			damager.DamageValue /2 :
			damager.DamageValue;
		if (Health <= 0) Die();
	}
	public void Die()
	{
		if (isDying) return;
		isDying = true;
		Destroy(characterControl);
		Destroy(controller);
		//Destroy(this);
		animator?.SetTrigger("Die");
		animator.applyRootMotion = true;
		StartCoroutine(DyingCoroutine(5).GetEnumerator());
	}

	IEnumerable DyingCoroutine(float secondsToWait)
	{
		yield return new WaitForSeconds(secondsToWait);
		Destroy(gameObject);
	}

	private void OnValidate()
	{
		healthBar = HealthBar as MonoBehaviour;
	}
}
