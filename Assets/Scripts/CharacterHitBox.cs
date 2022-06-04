using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitBox : MonoBehaviour
{
	[SerializeField]
	Character character;

	private void OnTriggerEnter(Collider other)
	{
		Damager damager = other.GetComponent<Damager>();
		if(damager != null && damager.Enabled)
		{
			character.ApplyDamage(damager.DamageValue);
		}
	}
}
